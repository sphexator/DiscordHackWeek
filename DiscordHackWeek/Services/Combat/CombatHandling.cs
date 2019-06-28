using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Entities.Combat;
using DiscordHackWeek.Extensions;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Database.Tables;
using DiscordHackWeek.Services.Experience;
using DiscordHackWeek.Shared.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DiscordHackWeek.Services.Combat
{
    public class CombatHandling : INService
    {
        private readonly LevelHandling _level;
        private readonly Random _random;

        public readonly MemoryCache Consumables
            = new MemoryCache(new MemoryCacheOptions
                {ExpirationScanFrequency = TimeSpan.FromMinutes(1)});

        public CombatHandling(Random random, LevelHandling level)
        {
            _random = random;
            _level = level;
        }

        public async Task<User> StartAsync(SocketCommandContext context, DbService db)
        {
            var user = new User
            {
                UserId = context.User.Id,
                Level = 1,
                Exp = 0,
                TotalExp = 0,
                Credit = 0,
                AttackMode = AttackType.Passive,
                ContinentId = 1,
                ZoneId = 1,
                UnspentTalentPoints = 0,
                DamageTalent = 0,
                HealthTalent = 0,
                WeaponId = 1,
                ArmorId = 2
            };
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            var firstContinent = await db.Continents.FindAsync(1);
            var firstZone = await db.Zones.FindAsync(1);
            await context.ReplyAsync($"Welcome to Wonderland {context.User.Mention}!\n" +
                                     $"You're starting off within the continent {firstContinent.Name} in {firstZone.Name} where you'll face off monsters to gain experience to level up and explore other zones. You can also send troops on missions or do quests to gain experience and loot!\n" +
                                     "For every second level you level up, you'll receive a new talent point which you can spend to increase your health, or damage.\n\n" +
                                     "Now, wait a bit and we'll resume back to the command and below is a list of basic commands to get started!\n" +
                                     "Commands:\n" +
                                     "-Search (Find an opponement)");
            await Task.Delay(2000);
            return user;
        }

        public async Task SearchAsync(SocketCommandContext context, DbService db)
        {
            var user = await db.Users.FindAsync(context.User.Id) 
                       ?? await StartAsync(context, db);

            var enemies = await db.Enemies.Where(x => x.ZoneId == user.ZoneId).ToListAsync();
            var found = _random.Next(100);
            if (found >= 40)
            {
                var enemy = enemies[_random.Next(enemies.Count)];
                var embed = new EmbedBuilder
                {
                    Color = Color.Green,
                    Description = $"{context.User.Mention} encountered a wild {enemy.Name}!",
                    ThumbnailUrl = enemy.Image
                };
                await context.ReplyAsync($"{context.User.Mention} encountered a wild {enemy.Name}!", Color.Green.RawValue);
                await BattleAsync(context, user, enemy, db);
            }
            else await context.ReplyAsync($"{context.User.Mention} searched around and found no one", Color.Red.RawValue);
        }

        public async Task BattleAsync(SocketCommandContext context, User userData, Enemy enemyData, DbService db)
        {
            var zone = await db.Zones.FirstOrDefaultAsync(x => x.Id == userData.ZoneId);
            var user = await BuildCombatUserAsync(context.User, userData, db);
            int lvl;
            if (userData.Level + 3 > zone.HighLevel) lvl = zone.HighLevel;
            else lvl = userData.Level + 3;
            var enemy = await BuildCombatUserAsync(enemyData, _random.Next(zone.LowLevel, lvl), db);

            var msgLog = new LinkedList<string>();
            msgLog.AddFirst($"{user.Name} VS {enemy.Name}");
            var msg = await context.ReplyAsync(msgLog.ListToString());
            var inventory = await db.Inventories.Where(x => x.UserId == context.User.Id).ToListAsync();
            var winner = await CombatAsync(msg, user, enemy, db, msgLog, inventory);
            if (winner.Name == enemy.Name) return;
            var exp = _level.AddExpAndCredit(enemyData.Exp + _random.Next(0, enemyData.Credit), enemyData.Credit, userData,
                out var response);
            var embed = msg.Embeds.First().ToEmbedBuilder();
            var update = false;
            if (exp)
            {
                update = true;
                embed.AddField("Exp", response, true);
            }

            if (enemyData.LootTableIds.Length != 0)
            {
                update = true;
                var loot = new List<Item>();
                var lootResponse = "";
                if (enemyData.Credit != 0) lootResponse += $"{enemyData.Credit} credit\n";
                var amount = enemyData.LootTableIds.Length > 2 ? 3 : enemyData.LootTableIds.Length;
                for (var i = 0; i < amount; i++)
                {
                    var item = await db.Items.FindAsync(enemyData.LootTableIds.ElementAt(_random.Next(enemyData.LootTableIds.Length)));
                    if (!loot.Contains(item))
                    {
                        loot.Add(item);
                        lootResponse += $"{item.Name}\n";
                        var invItem = inventory.FirstOrDefault(x => x.ItemId == item.Id);
                        if (invItem != null)
                        {
                            if (!item.Unique) invItem.Amount++;
                        }
                        else
                        {
                            await db.Inventories.AddAsync(new Inventory
                            {
                                UserId = context.User.Id,
                                ItemId = item.Id,
                                Amount = 1,
                                ItemType = item.ItemType
                            });
                        }
                    }
                    else
                    {
                        i--;
                    }
                }

                embed.AddField("Loot", lootResponse, true);
            }

            if (update) await msg.ModifyAsync(x => x.Embed = embed.Build());
            await db.SaveChangesAsync();
        }

        private async Task<CombatUser> CombatAsync(IUserMessage msg, CombatUser user, CombatUser enemy, DbService db,
            LinkedList<string> msgLog, List<Inventory> userInventory)
        {
            EmbedBuilder embed;
            while (true)
            {
                // User always goes first
                if (Convert.ToInt32(user.DmgTaken / user.Health * 100) >= 70
                    && userInventory.Count(x => x.ItemType == ItemType.Food && x.ItemType == ItemType.Food) >
                    0)
                {
                    var potionId = userInventory.FirstOrDefault(x =>
                        x.ItemType == ItemType.Potion);
                    if (potionId != null)
                    {
                        var potion = await db.Items.FindAsync(potionId.ItemId);
                        UpdateBattleLog(msgLog,
                            $"{user.Name} drank a {potion.Name} and regained {potion.HealthIncrease} health!");
                        user.DmgTaken -= potion.HealthIncrease;
                        if (potionId.Amount == 1) db.Inventories.Remove(potionId);
                        else potionId.Amount--;
                    }
                    else
                    {
                        var foodId = userInventory.FirstOrDefault(x =>
                            x.ItemType == ItemType.Food);
                        if (foodId != null)
                        {
                            var food = await db.Items.FindAsync(foodId.ItemId);
                            UpdateBattleLog(msgLog,
                                $"{user.Name} ate {food.Name} and regained {food.Name} health!");
                            user.DmgTaken -= food.HealthIncrease;
                            if (foodId.Amount == 1) db.Inventories.Remove(foodId);
                            else foodId.Amount--;
                        }
                    }
                }
                else
                {
                    var usDmg = CalculateDamage(user);

                    enemy.DmgTaken += usDmg;
                    if (enemy.DmgTaken >= enemy.Health)
                    {
                        UpdateBattleLog(msgLog, $"{user.Name} hit for {usDmg} damage and defeated {enemy.Name}");
                        embed = msg.Embeds.First().ToEmbedBuilder();
                        embed.Description = msgLog.ListToString();
                        await msg.ModifyAsync(x => x.Embed = embed.Build());
                        await Task.Delay(2000);
                        return user;
                    }

                    UpdateBattleLog(msgLog, $"{user.Name} hit {enemy.Name} for {usDmg} damage");
                }

                var enDmg = CalculateDamage(enemy);
                user.DmgTaken += enDmg;
                if (user.DmgTaken < user.Health)
                {
                    UpdateBattleLog(msgLog, $"{enemy.Name} hit {user.Name} for {enDmg} damage");
                    embed = msg.Embeds.First().ToEmbedBuilder();
                    embed.Description = msgLog.ListToString();
                    await msg.ModifyAsync(x => x.Embed = embed.Build());
                    await Task.Delay(2000);
                    continue;
                }

                UpdateBattleLog(msgLog, $"{enemy.Name} hit for {enDmg} damage and defeated {user.Name}");
                UpdateBattleLog(msgLog, "You died :(");
                embed = msg.Embeds.First().ToEmbedBuilder();
                embed.Description = msgLog.ListToString();
                embed.Color = Color.Red;
                await msg.ModifyAsync(x => x.Embed = embed.Build());
                await Task.Delay(2000);
                return enemy;
            }
        }

        private int CalculateDamage(CombatUser user)
        {
            var critChance = _random.Next(100);
            var dmg = 1 * user.AttackPower;
            if (critChance >= user.CriticalChance) dmg = Convert.ToInt32(dmg * 1.5);

            var lowDmg = dmg / 1.5;
            if (lowDmg <= 0) lowDmg = 5;
            var highDmg = dmg * 1.5;
            if (lowDmg >= highDmg) highDmg = lowDmg + 5;

            return _random.Next(Convert.ToInt32(lowDmg), Convert.ToInt32(highDmg));
        }

        private void UpdateBattleLog(LinkedList<string> log, string message)
        {
            if (log.Count == 6) log.RemoveLast();
            log.AddFirst(message);
        }

        public async Task<CombatUser> BuildCombatUserAsync(SocketUser socketUser, User userData, DbService db)
        {
            var weapon = await db.Items.FindAsync(userData.WeaponId);
            var armor = await db.Items.FindAsync(userData.ArmorId);
            var user = new CombatUser
            {
                Name = socketUser.Username,
                Health = 10 * userData.Level + 10 * userData.HealthTalent + 10 * armor.HealthIncrease,
                AttackPower = 1 * userData.Level + 10 * userData.DamageTalent + 10 * weapon.DamageIncrease +
                              10 * armor.DamageIncrease,
                CriticalChance = 15 + 1 * weapon.CritIncrease + 10 * armor.CritIncrease,
                DmgTaken = 0
            };
            if (Consumables.TryGetValue(userData.UserId, out var item) && item is Item consum)
            {
                user.AttackPower += 10 * consum.DamageIncrease;
                user.CriticalChance += 1 * consum.CritIncrease;
                user.Health += 10 * consum.HealthIncrease;
            }

            return user;
        }

        private async Task<CombatUser> BuildCombatUserAsync(Enemy enemyData, int level, DbService db)
        {
            var enemy = new CombatUser
            {
                Name = enemyData.Name,
                Health = 15 * level,
                AttackPower = 1 * level,
                DmgTaken = 0,
                CriticalChance = 10
            };
            if (enemyData.ArmorId.HasValue)
            {
                var eArmor = await db.Items.FindAsync(enemyData.ArmorId);
                enemy.Health += 10 * eArmor.HealthIncrease;
                enemy.CriticalChance += 1 * eArmor.CritIncrease;
                enemy.AttackPower += 10 * eArmor.DamageIncrease;
            }

            if (!enemyData.WeaponId.HasValue) return enemy;
            var eWeapon = await db.Items.FindAsync(enemyData.WeaponId);
            enemy.AttackPower += 10 * eWeapon.DamageIncrease;
            enemy.CriticalChance += 1 * eWeapon.CritIncrease;

            return enemy;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Entities.Combat;
using DiscordHackWeek.Extensions;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Database.Tables;
using DiscordHackWeek.Shared.Command;
using Microsoft.Extensions.Caching.Memory;

namespace DiscordHackWeek.Services.Combat
{
    public class CombatHandling : INService
    {
        private readonly Random _random;

        public readonly MemoryCache Consumables
            = new MemoryCache(new MemoryCacheOptions
                { ExpirationScanFrequency = TimeSpan.FromMinutes(1) });

        public CombatHandling(Random random) => _random = random;

        public async Task BattleAsync(SocketCommandContext context, User userData, Enemy enemyData, DbService db)
        {
            var user = await BuildCombatUserAsync(userData, db);
            var enemy = await BuildCombatUserAsync(enemyData, db);
            var msgLog = new LinkedList<string>();
            msgLog.AddFirst($"{user.Name} VS {enemy.Name}");
            var msg = await context.ReplyAsync(msgLog.ListToString());
            var winner = await CombatAsync(msg, user, enemy, msgLog);
        }

        private async Task<CombatUser> CombatAsync(IUserMessage msg, CombatUser user, CombatUser enemy, LinkedList<string> msgLog)
        {
            EmbedBuilder embed;
            while (true)
            {
                // User always goes first
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

        private async Task<CombatUser> BuildCombatUserAsync(User userData, DbService db)
        {
            var weapon = await db.Items.FindAsync(userData.WeaponId);
            var armor = await db.Items.FindAsync(userData.ArmorId);
            var user = new CombatUser
            {
                Name = "Wumpus",
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

        private async Task<CombatUser> BuildCombatUserAsync(Enemy enemyData, DbService db)
        {
            var enemy = new CombatUser
            {
                Name = enemyData.Name,
                Health = 15 * enemyData.Level,
                AttackPower = 1 * enemyData.Level,
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
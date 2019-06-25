using System;
using System.Threading.Tasks;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Entities.Combat;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Database.Tables;
using Microsoft.Extensions.Caching.Memory;

namespace DiscordHackWeek.Services.Combat
{
    public class CombatHandling : INService
    {
        private readonly Random _random;

        public readonly MemoryCache Consumables
            = new MemoryCache(new MemoryCacheOptions
                {ExpirationScanFrequency = TimeSpan.FromMinutes(1)});

        public CombatHandling(Random random) => _random = random;

        public async Task Battle(User userData, Enemy enemyData, DbService db)
        {
            var user = BuildCombatUser(userData, db);
            var enemy = BuildCombatUser(enemyData, db);
        }

        private async Task<CombatUser> BuildCombatUser(User userData, DbService db)
        {
            var weapon = await db.Items.FindAsync(userData.WeaponId);
            var armor = await db.Items.FindAsync(userData.ArmorId);
            var user = new CombatUser
            {
                Health = 10 * userData.Level + 10 * userData.HealthTalent + 10 * armor.HealthIncrease,
                AttackPower = 1 * userData.Level + 10 * userData.DamageTalent + 10 * weapon.DamageIncrease +
                              10 * armor.DamageIncrease,
                CriticalChance = 1 + 10 * weapon.CritIncrease + 10 * armor.CritIncrease,
                DmgTaken = 0
            };
            if (Consumables.TryGetValue(userData.UserId, out var item) && item is Item consum)
            {
                user.AttackPower += 10 * consum.DamageIncrease;
                user.CriticalChance += 10 * consum.CritIncrease;
                user.Health += 10 * consum.HealthIncrease;
            }

            return user;
        }

        private async Task<CombatUser> BuildCombatUser(Enemy enemyData, DbService db)
        {
            var enemy = new CombatUser
            {
                Health = 15 * enemyData.Level,
                AttackPower = 1 * enemyData.Level,
                DmgTaken = 0
            };
            if (enemyData.ArmorId.HasValue)
            {
                var eArmor = await db.Items.FindAsync(enemyData.ArmorId);
                enemy.Health += 10 * eArmor.HealthIncrease;
                enemy.CriticalChance += 10 * eArmor.CritIncrease;
                enemy.AttackPower += 10 * eArmor.DamageIncrease;
            }

            if (!enemyData.WeaponId.HasValue) return enemy;
            var eWeapon = await db.Items.FindAsync(enemyData.WeaponId);
            enemy.AttackPower += 10 * eWeapon.DamageIncrease;
            enemy.CriticalChance += 10 * eWeapon.CritIncrease;

            return enemy;
        }

        private int CalculateDamage(CombatUser user)
        {
            return 0;
        }
    }
}
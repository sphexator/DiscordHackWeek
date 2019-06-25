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
        public readonly MemoryCache Consumables 
            = new MemoryCache(new MemoryCacheOptions
                { ExpirationScanFrequency = TimeSpan.FromMinutes(1) });
        
        public async Task Battle(User userData, Enemy enemy, DbService db)
        {
            var user = new CombatUser
            {
                 Health = (10 * userData.Level) + (10 * userData.HealthTalent)
            };
        }

        private int CalculateDamage(CombatUser user)
        {
            
            return 0;
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DiscordHackWeek.Services.Combat
{
    public class CombatService
    {
        public readonly MemoryCache Consumables 
            = new MemoryCache(new MemoryCacheOptions
                { ExpirationScanFrequency = TimeSpan.FromMinutes(1) });

        public async Task Enemy()
        {

        }

        public async Task Battle()
        {

        }

        public int Damage()
        {

            return 0;
        }
    }
}

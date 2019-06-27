using System.Collections.Generic;
using DiscordHackWeek.Entities.Combat;

namespace DiscordHackWeek.Services.Database.Tables
{
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZoneId { get; set; }
        public string Image { get; set; }
        public EnemyType Type { get; set; }
        public int Level { get; set; }
        public string LightAttack { get; set; }
        public string HeavyAttack { get; set; }

        public int Exp { get; set; } = 5;
        public int Credit { get; set; } = 0;
        // public ICollection<LootTable> Loot { get; set; }
        public List<int> LootTableIds { get; set; }
        public int RareDropChance { get; set; } = 1;
        public int? WeaponId { get; set; }
        public int? ArmorId { get; set; }
    }
}
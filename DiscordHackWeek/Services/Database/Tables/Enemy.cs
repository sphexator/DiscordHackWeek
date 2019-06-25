using System.Collections.Generic;
using DiscordHackWeek.Entities.Combat;

namespace DiscordHackWeek.Services.Database.Tables
{
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EnemyType Type { get; set; }
        public int Level { get; set; }
        public string LightAttack { get; set; }
        public string HeavyAttack { get; set; }

        public int Exp { get; set; }
        public ICollection<LootTable> Loot { get; set; }
        public int? WeaponId { get; set; }
        public int? ArmorId { get; set; }
    }
}
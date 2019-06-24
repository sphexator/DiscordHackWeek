using DiscordHackWeek.Entities;

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
    }
}
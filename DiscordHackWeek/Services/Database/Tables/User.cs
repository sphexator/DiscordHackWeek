using DiscordHackWeek.Entities.Combat;

namespace DiscordHackWeek.Services.Database.Tables
{
    public class User
    {
        public ulong UserId { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public int TotalExp { get; set; } = 0;
        public AttackType AttackMode { get; set; } = AttackType.Passive;
        public int ContinentId { get; set; } = 1;
        public int ZoneId { get; set; } = 1;
        public int UnspentTalentPoints { get; set; } = 0;
        public int DamageTalent { get; set; } = 0;
        public int HealthTalent { get; set; } = 0;

        public int WeaponId { get; set; } = 1;
        public int ArmorId { get; set; } = 2;
    }
}
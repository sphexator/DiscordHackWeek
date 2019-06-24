namespace DiscordHackWeek.Services.Database.Tables
{
    public class User
    {
        public ulong UserId { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int TotalExp { get; set; }
        public int ContinentId { get; set; }
        public int ZoneId { get; set; }
        public int UnspentTalentPoints { get; set; }
        public int DamageTalent { get; set; }
        public int HealthTalent { get; set; }
    }
}
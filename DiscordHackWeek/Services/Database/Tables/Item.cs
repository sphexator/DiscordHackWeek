namespace DiscordHackWeek.Services.Database.Tables
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Unique { get; set; }

        public int HealthIncrease { get; set; }
        public int DamageIncrease { get; set; }
        public int CritIncrease { get; set; }
    }
}
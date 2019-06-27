namespace DiscordHackWeek.Services.Database.Tables
{
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LowLevel { get; set; }
        public int HighLevel { get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
        public int ContinentId { get; set; }
    }
}
namespace DiscordHackWeek.Services.Database.Tables
{
    public class Inventory
    {
        public ulong UserId { get; set; }
        public int Amount { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
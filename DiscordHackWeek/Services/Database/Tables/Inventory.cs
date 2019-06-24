namespace DiscordHackWeek.Services.Database.Tables
{
    public class Inventory
    {
        public Inventory(ulong userId, Item item)
        {
            UserId = userId;
            Item = item;
            ItemId = item.Id;
            Amount = 1;
        }

        public ulong UserId { get; set; }
        public int Amount { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
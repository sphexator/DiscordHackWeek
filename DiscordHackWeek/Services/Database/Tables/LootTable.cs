namespace DiscordHackWeek.Services.Database.Tables
{
    public class LootTable
    {
        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
namespace DiscordHackWeek.Services.Database.Tables.Objective
{
    public class Quest
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int EnemyId { get; set; }
        public int Amount { get; set; }
        public int CreditReward { get; set; }
        public int ExpReward { get; set; }
        public int[] LootReward { get; set; }
    }
}
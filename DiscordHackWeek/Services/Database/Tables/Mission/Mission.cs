using System;

namespace DiscordHackWeek.Services.Database.Tables.Mission
{
    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public int CreditReward { get; set; } = 0;
        public int ExpReward { get; set; }
        public int[] LootRewards { get; set; }
        public bool Active { get; set; } = false;
    }
}

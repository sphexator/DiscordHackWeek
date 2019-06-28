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

        public string[] SuccessfulResponse { get; set; }
        public string[] FailedResponse { get; set; }

        public bool Active { get; set; } = false;
        public TimeSpan ActiveSpan { get; set; }
        public DateTimeOffset ActiveSince { get; set; }
    }
}

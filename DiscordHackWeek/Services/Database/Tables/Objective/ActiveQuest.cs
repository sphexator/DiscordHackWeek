namespace DiscordHackWeek.Services.Database.Tables.Objective
{
    public class ActiveQuest
    {
        public ulong UserId { get; set; }
        public int QuestId { get; set; }
        public int Progress { get; set; }
    }
}
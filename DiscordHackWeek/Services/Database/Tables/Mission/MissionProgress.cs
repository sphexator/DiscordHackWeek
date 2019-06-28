using System;

namespace DiscordHackWeek.Services.Database.Tables.Mission
{
    public class MissionProgress
    {
        public ulong UserId { get; set; }
        public int MissionId { get; set; }
        public int SuccessChance { get; set; }
        public bool Success { get; set; }
        public DateTimeOffset Started { get; set; }
    }
}

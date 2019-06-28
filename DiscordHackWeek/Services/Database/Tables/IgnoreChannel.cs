using System.Collections.Generic;

namespace DiscordHackWeek.Services.Database.Tables
{
    public class IgnoreChannel
    {
        public ulong GuildId { get; set; }
        public List<ulong> IgnoreList { get; set; }
    }
}
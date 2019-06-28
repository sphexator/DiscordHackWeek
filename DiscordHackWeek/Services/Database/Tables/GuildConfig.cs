using System.Collections.Generic;

namespace DiscordHackWeek.Services.Database.Tables
{
    public class GuildConfig
    {
        public ulong GuildId { get; set; }
        public List<string> Prefixes { get; set; }
    }
}
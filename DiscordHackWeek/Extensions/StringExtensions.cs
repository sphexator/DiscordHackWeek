using System.Collections.Generic;

namespace DiscordHackWeek.Extensions
{
    public static class StringExtensions
    {
        public static string ListToString(this IEnumerable<string> list) => string.Join("\n", list);
    }
}

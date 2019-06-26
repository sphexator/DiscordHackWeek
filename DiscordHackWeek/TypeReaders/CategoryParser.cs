using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Shared.Command;
using Qmmands;

namespace DiscordHackWeek.TypeReaders
{
    public class CategoryParser : Shared.Command.TypeParser<SocketCategoryChannel>
    {
        public override ValueTask<TypeParserResult<SocketCategoryChannel>> ParseAsync(Parameter parameter, string value, SocketCommandContext context, IServiceProvider provider)
        {
            if (ulong.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var id))
            {
                var channelId = context.Guild.GetCategoryChannel(id);
                return channelId == null
                    ? TypeParserResult<SocketCategoryChannel>.Unsuccessful("No category found")
                    : TypeParserResult<SocketCategoryChannel>.Successful(channelId);
            }

            var channel = context.Guild.CategoryChannels.FirstOrDefault(x => x.Name == value);
            return channel == null
                ? TypeParserResult<SocketCategoryChannel>.Unsuccessful("No category found")
                : TypeParserResult<SocketCategoryChannel>.Successful(channel);
        }
    }
}

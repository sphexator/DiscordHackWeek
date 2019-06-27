using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Qmmands;

namespace DiscordHackWeek.Shared.Command
{
    public class SocketCommandContext : CommandContext
    {
        public SocketCommandContext(DiscordSocketClient client, SocketUserMessage msg, SocketUser user)
        {
            Client = client;
            Message = msg;
            User = user as SocketGuildUser;
            Guild = (user as SocketGuildUser)?.Guild;
            Channel = msg.Channel;
        }

        public SocketUserMessage Message { get; }
        public DiscordSocketClient Client { get; }
        public SocketGuildUser User { get; }
        public SocketGuild Guild { get; }
        public ISocketMessageChannel Channel { get; }

        public async Task<IUserMessage> ReplyAsync(string content, uint? color = null)
        {
            if (!color.HasValue) color = Color.Purple.RawValue;
            return await Channel.SendMessageAsync(null, false, new EmbedBuilder
            {
                Description = content,
                Color = new Color(color.Value)
            }.Build());
        }

        public async Task<IUserMessage> ReplyAsync(EmbedBuilder embed, uint? color = null)
        {
            if (!color.HasValue) color = Color.Purple.RawValue;
            return await Channel.SendMessageAsync(null, false, embed.Build());
        }
    }
}

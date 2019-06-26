using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Shared.Command;
using DiscordHackWeek.Interactive.Criteria;

namespace DiscordHackWeek.Interactive.Paginator
{
    internal class EnsureReactionFromSourceUserCriterion : ICriterion<SocketReaction>
    {
        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, SocketReaction parameter)
        {
            var ok = parameter.UserId == sourceContext.User.Id;
            return Task.FromResult(ok);
        }
    }
}
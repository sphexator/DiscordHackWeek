using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Entities.Command;
using DiscordHackWeek.Interactive.Criteria;

namespace DiscordHackWeek.Interactive.Paginator
{
    internal class EnsureIsIntegerCriterion : ICriterion<SocketMessage>
    {
        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, SocketMessage parameter)
        {
            var ok = int.TryParse(parameter.Content, out _);
            return Task.FromResult(ok);
        }
    }
}
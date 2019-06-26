using System.Threading.Tasks;
using DiscordHackWeek.Entities.Command;

namespace DiscordHackWeek.Interactive.Criteria
{
    public class EmptyCriterion<T> : ICriterion<T>
    {
        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, T parameter)
        {
            return Task.FromResult(true);
        }
    }
}
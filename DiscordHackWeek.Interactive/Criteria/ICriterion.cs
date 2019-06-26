using System.Threading.Tasks;
using DiscordHackWeek.Shared.Command;

namespace DiscordHackWeek.Interactive.Criteria
{
    public interface ICriterion<in T>
    {
        Task<bool> JudgeAsync(SocketCommandContext sourceContext, T parameter);
    }
}
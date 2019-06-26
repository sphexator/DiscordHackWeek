using System.Threading.Tasks;
using DiscordHackWeek.Entities.Command;

namespace DiscordHackWeek.Interactive.Criteria
{
    public interface ICriterion<in T>
    {
        Task<bool> JudgeAsync(SocketCommandContext sourceContext, T parameter);
    }
}
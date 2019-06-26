using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Entities.Command;
using DiscordHackWeek.Interactive.Criteria;
using Qmmands;

namespace DiscordHackWeek.Interactive.Callbacks
{
    public interface IReactionCallback
    {
        RunMode RunMode { get; }
        ICriterion<SocketReaction> Criterion { get; }
        TimeSpan? Timeout { get; }
        SocketCommandContext Context { get; }

        Task<bool> HandleCallbackAsync(SocketReaction reaction);
    }
}
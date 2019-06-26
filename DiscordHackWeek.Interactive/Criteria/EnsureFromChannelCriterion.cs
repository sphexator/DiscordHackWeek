﻿using System.Threading.Tasks;
using Discord;
using DiscordHackWeek.Shared.Command;

namespace DiscordHackWeek.Interactive.Criteria
{
    public class EnsureFromChannelCriterion : ICriterion<IMessage>
    {
        private readonly ulong _channelId;

        public EnsureFromChannelCriterion(IMessageChannel channel)
        {
            _channelId = channel.Id;
        }

        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, IMessage parameter)
        {
            var ok = _channelId == parameter.Channel.Id;
            return Task.FromResult(ok);
        }
    }
}
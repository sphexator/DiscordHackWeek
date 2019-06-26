﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Qmmands;

namespace DiscordHackWeek.Entities.Command
{
    public abstract class PreconditionAttribute : CheckAttribute
    {
        public override ValueTask<CheckResult> CheckAsync(CommandContext context, IServiceProvider provider)
            => CheckAsync((SocketCommandContext)context, provider);

        public abstract ValueTask<CheckResult> CheckAsync(SocketCommandContext context, IServiceProvider provider);
    }
}

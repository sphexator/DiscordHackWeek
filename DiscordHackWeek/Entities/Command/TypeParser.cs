using System;
using System.Threading.Tasks;
using Qmmands;

namespace DiscordHackWeek.Entities.Command
{
    public abstract class TypeParser<T> : Qmmands.TypeParser<T>
    {
        public override ValueTask<TypeParserResult<T>> ParseAsync(Parameter parameter, string value,
            CommandContext context, IServiceProvider provider)
            => ParseAsync(parameter, value, (SocketCommandContext)context, provider);

        public abstract ValueTask<TypeParserResult<T>> ParseAsync(Parameter parameter, string value, SocketCommandContext context,
            IServiceProvider provider);
    }
}
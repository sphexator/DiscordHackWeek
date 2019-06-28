using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Qmmands;

namespace DiscordHackWeek.Services
{
    public class LogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        public LogService(DiscordSocketClient client, CommandService command, ILogger<LogService> logger)
        {
            _client = client;
            _command = command;
            _logger = logger;

            _client.Log += DiscordLog;
            _command.CommandErrored += CommandError;
        }

        private Task CommandError(CommandErroredEventArgs e)
        {
            _logger.Log(LogLevel.Error, e.Result.Exception, $"{e.Result.Reason} - {e.Result.CommandExecutionStep}");
            return Task.CompletedTask;
        }

        private Task DiscordLog(LogMessage log)
        {
            _logger.Log(LogSevToLogLevel(log.Severity), log.Exception, log.Message);
            return Task.CompletedTask;
        }

        private LogLevel LogSevToLogLevel(LogSeverity log)
        {
            switch (log)
            {
                case LogSeverity.Critical:
                    return LogLevel.Critical;
                case LogSeverity.Error:
                    return LogLevel.Error;
                case LogSeverity.Warning:
                    return LogLevel.Warning;
                case LogSeverity.Info:
                    return LogLevel.Information;
                case LogSeverity.Verbose:
                    return LogLevel.Trace;
                case LogSeverity.Debug:
                    return LogLevel.Trace;
                default:
                    return LogLevel.None;
            }
        }
    }
}

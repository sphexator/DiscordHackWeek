using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordHackWeek.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Qmmands;

namespace DiscordHackWeek
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, DiscordSocketClient client, CommandService command, IConfiguration config, IServiceProvider provider)
        {
            _logger = logger;
            _client = client;
            _command = command;
            _config = config;
            _provider = provider;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var assembly = Assembly.GetEntryAssembly();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                _command.AddModules(assembly);
                _provider.GetRequiredService<CommandHandling>();
                _provider.GetRequiredService<LogService>();
                await _client.LoginAsync(TokenType.Bot, _config["Token"]);
                await _client.StartAsync();
                await Task.Delay(-1, stoppingToken);
            }
        }
    }
}
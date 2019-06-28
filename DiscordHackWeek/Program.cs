using System;
using Discord;
using Discord.WebSocket;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Interactive;
using DiscordHackWeek.Services;
using DiscordHackWeek.Services.Combat;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Experience;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qmmands;

namespace DiscordHackWeek
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    DbWabWab.DbCon = hostContext.Configuration["DbCon"];
                    services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                    {
                        AlwaysDownloadUsers = true,
                        LogLevel = LogSeverity.Info,
                        MessageCacheSize = 35
                    }));
                    services.AddSingleton(new CommandService(new CommandServiceConfiguration
                    {
                        DefaultRunMode = RunMode.Parallel
                    }));
                    services.AddSingleton<CommandHandling>();
                    services.AddSingleton<CombatHandling>();
                    services.AddSingleton<LevelHandling>();
                    services.AddSingleton<LogService>();
                    services.AddSingleton<Random>();
                    services.AddSingleton<InteractiveService>();
                    services.AddLogging();
                    services.AddSingleton(hostContext.Configuration);
                });
    }
}
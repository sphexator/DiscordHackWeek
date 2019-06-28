using Discord;
using Discord.WebSocket;
using DiscordHackWeek.Services.Database;
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
                    services.AddDbContextPool<DbService>(e =>
                    {
                        e.UseNpgsql(hostContext.Configuration["DbCon"], x => x.EnableRetryOnFailure(5));
                    }, 250);
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
                    services.AddLogging();
                });
    }
}
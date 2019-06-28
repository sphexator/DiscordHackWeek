using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordHackWeek
{
    public class MissionWorker : BackgroundService
    {
        private readonly ILogger<MissionWorker> _logger;
        private readonly DiscordSocketClient _client;
        private readonly Random _random;

        public MissionWorker(ILogger<MissionWorker> logger, DiscordSocketClient client, Random random)
        {
            _logger = logger;
            _client = client;
            _random = random;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                using (var db = new DbService())
                {
                    await CompleteMission(db, stoppingToken);
                    var toInactive = await db.Missions
                        .Where(x => x.Active && x.ActiveSince + x.ActiveSpan <= DateTimeOffset.UtcNow).ToListAsync(stoppingToken);
                    var toActive = await db.Missions
                        .Where(x => !x.Active && x.ActiveSince + (x.ActiveSpan * 2) <= DateTimeOffset.UtcNow).ToListAsync(stoppingToken);
                    for (var i = 0; i < toInactive.Count; i++)
                    {
                        var x = toInactive[i];
                        x.Active = false;

                        var removeMission = await db.MissionCompleted.Where(e => e.MissionId == x.Id).ToListAsync(stoppingToken);
                        db.RemoveRange(removeMission);
                    }
                    for (var i = 0; i < toActive.Count; i++)
                    {
                        var x = toActive[i];
                        x.Active = true;
                        x.ActiveSince = DateTimeOffset.UtcNow;
                    }

                    await db.SaveChangesAsync(stoppingToken);
                }
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task CompleteMission(DbService db, CancellationToken stoppingToken)
        {
            foreach (var x in await db.MissionProgress.ToListAsync(stoppingToken))
            {
                var mission = await db.Missions.FindAsync(x.MissionId);
                if(x.Started + mission.Duration > DateTimeOffset.UtcNow) continue;

                x.Success = _random.Next(100) <= x.SuccessChance;
            }

            await db.SaveChangesAsync(stoppingToken);
        }
    }
}
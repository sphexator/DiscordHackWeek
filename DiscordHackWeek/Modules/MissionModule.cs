using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discord;
using DiscordHackWeek.Extensions;
using DiscordHackWeek.Interactive;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Database.Tables.Mission;
using DiscordHackWeek.Services.Experience;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Qmmands;

namespace DiscordHackWeek.Modules
{
    [Name("Mission")]
    public class MissionModule : InteractiveBase
    {
        private readonly LevelHandling _level;
        public MissionModule(LevelHandling level) => _level = level;

        [Name("Accept Mission")]
        [Description("Send the Wumpus troop on a mission")]
        [Command("am")]
        public async Task AcceptMissionAsync(int id)
        {
            using var db = new DbService();
            var mission = await db.Missions.FindAsync(id);
            if (mission == null || !mission.Active)
            {
                await Context.ReplyAsync("No mission found with that ID", Color.Red.RawValue);
                return;
            }

            var progMission = await db.MissionProgress.FindAsync(Context.User.Id, mission.Id);
            var compMission = await db.MissionCompleted.FindAsync(Context.User.Id, mission.Id);
            if (progMission != null || compMission != null)
            {
                await Context.ReplyAsync("You're already on this mission or completed it. Wait a bit till you have completed it and its available again.", Color.Red.RawValue);
                return;
            }

            var current = DateTimeOffset.UtcNow - mission.ActiveSince;
            var chance = Convert.ToInt32(current.TotalMinutes / mission.ActiveSpan.TotalMinutes * 100);
            await db.MissionProgress.AddAsync(new MissionProgress
            {
                MissionId = mission.Id,
                Success = false,
                UserId = Context.User.Id,
                SuccessChance = chance,
                Completed = false,
                Started = DateTimeOffset.UtcNow
            });
            await db.SaveChangesAsync();
            await Context.ReplyAsync($"Started mission {mission.Name}! It'll be done in {mission.Duration.Humanize()}");
        }

        [Name("List Missions")]
        [Description("See all missions available")]
        [Command("lm")]
        public async Task ListMissionsAsync()
        {
            using var db = new DbService();
            var activeMissions = await db.Missions.Where(x => x.Active).ToListAsync();
            var userMissions = await db.MissionProgress.Where(x => x.UserId == Context.User.Id).ToListAsync();
            var completedMissions = await db.MissionCompleted.Where(x => x.UserId == Context.User.Id).ToListAsync();
            var result = new List<string>();
            for (var i = 0; i < activeMissions.Count; i++)
            {
                var x = activeMissions[i];
                if(userMissions.FirstOrDefault(e => e.MissionId == x.Id) != null) continue;
                if(completedMissions.FirstOrDefault(e => e.MissionId == x.Id) != null) continue;
                result.Add($"ID: {x.Id} - {x.Name} - Duration: {x.Duration.Humanize()}\n" +
                           $"Reward: Exp {x.ExpReward} - Credit {x.CreditReward}");
            }

            await PagedReplyAsync(result.PaginateBuilder(Context.Guild, "Missions Available", null));
        }

        [Name("Claim Completed Mission")]
        [Description("Complete all missions")]
        [Command("ccm")]
        public async Task CompleteMissionsAsync()
        {
            using var db = new DbService();
            var completedMissions =
                await db.MissionProgress.Where(x => x.UserId == Context.User.Id && x.Completed).ToListAsync();
            var result = new List<string>();
            var user = await db.Users.FindAsync(Context.User.Id);
            if (user == null || completedMissions.Count == 0)
            {
                await Context.ReplyAsync("No completed missions to claim", Color.Red.RawValue);
                return;
            }
            for (var i = 0; i < completedMissions.Count; i++)
            {
                var x = completedMissions[i];
                var mission = await db.Missions.FindAsync(x.MissionId);
                if(!x.Success) result.Add($"({x.MissionId}) {mission.Name} Failed...");
                else
                { 
                    result.Add($"({x.MissionId}) {mission.Name} Completed. Gained {mission.ExpReward} exp & {mission.CreditReward} credit");
                    _level.AddExpAndCredit(mission.ExpReward, mission.CreditReward, user, out _);
                }
                await db.MissionCompleted.AddAsync(new MissionCompleted { UserId = x.UserId, MissionId = x.MissionId });
            }
            db.RemoveRange(completedMissions);
            await db.SaveChangesAsync();
            await PagedReplyAsync(result.PaginateBuilder(Context.Guild, "Missions Completed", null));
        }
    }
}
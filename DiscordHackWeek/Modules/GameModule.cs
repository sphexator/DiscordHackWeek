using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordHackWeek.Extensions;
using DiscordHackWeek.Interactive;
using DiscordHackWeek.Interactive.Paginator;
using DiscordHackWeek.Services.Combat;
using DiscordHackWeek.Services.Database;
using Microsoft.EntityFrameworkCore;
using Qmmands;

namespace DiscordHackWeek.Modules
{
    public class GameModule : InteractiveBase
    {
        private readonly CombatHandling _combat;
        public GameModule(CombatHandling combat) => _combat = combat;

        [Name("Search")]
        [Description("")]
        [Command("search")]
        public async Task PlayAsync()
        {
            using var db = new DbService();
            await _combat.SearchAsync(Context, db);
        }

        [Name("GoTo")]
        [Description("")]
        [Command("goto", "travel")]
        [Priority(2)]
        public async Task TravelAsync([Remainder] string name)
        {
            using var db = new DbService();
            var zone = await db.Zones.FirstOrDefaultAsync(x => x.Name == name);
            if (zone == null)
            {

                return;
            }

            var user = await db.Users.FindAsync(Context.User.Id);
            await Context.ReplyAsync($"Wanna move over to {zone.Name}? (y/n)");
            var response = await NextMessageAsync();
            if (response == null || response.Content.ToLower() != "y") return;
            user.ZoneId = zone.Id;
            await db.SaveChangesAsync();
            await Context.ReplyAsync($"Changed zone to {zone.Name}!");
        }

        [Name("GoTo")]
        [Description("")]
        [Command("goto", "travel")]
        [Priority(1)]
        public async Task TravelAsync(int id)
        {
            using var db = new DbService();
            var zone = await db.Zones.FirstOrDefaultAsync(x => x.Id == id);
            if (zone == null)
            {

                return;
            }

            var user = await db.Users.FindAsync(Context.User.Id);
            await Context.ReplyAsync($"Wanna move over to {zone.Name}? (y/n)");
            var response = await NextMessageAsync();
            if (response == null || response.Content.ToLower() != "y") return;
            user.ZoneId = zone.Id;
            await db.SaveChangesAsync();
            await Context.ReplyAsync($"Changed zone to {zone.Name}!");
        }

        [Name("Zones")]
        [Description("")]
        [Command("zones")]
        public async Task ZonesAsync()
        {
            using var db = new DbService();
            var zones = new List<ImagePager>();
            foreach (var x in db.Zones)
            {
                zones.Add(new ImagePager
                {
                    Image = x.ImageSrc,
                    Content = x.Description
                });
            }

            await PagedReplyAsync(new PaginatedMessage
            {
                Color = Color.Purple,
                Pages = zones
            });
        }

        [Name("Profile")]
        [Description("")]
        [Command("Profile")]
        public async Task ProfileAsync(SocketGuildUser user = null)
        {
            if (user == null) user = Context.User;
        }

        [Name("Inventory")]
        [Description("")]
        [Command("inventory", "inv")]
        public async Task InventoryAsync()
        {
            using var db = new DbService();
            var inventory = await db.Inventories
                .Where(x => x.UserId == Context.User.Id).ToListAsync();
            if (inventory.Count == 0)
            {
                await Context.ReplyAsync("Your inventory is empty");
                return;
            }

            var result = new List<string>();
            for (var i = 0; i < inventory.Count; i++)
            {
                var x = inventory[i];
                var item = await db.Items.FirstOrDefaultAsync(z => z.Id == x.ItemId);
                if (item == null) continue;
                result.Add($"{item.Name} - Amount: {x.Amount}");
            }

            if (result.Count == 0)
            {
                await Context.ReplyAsync("Your inventory is empty");
                return;
            }

            await PagedReplyAsync(result.PaginateBuilder(Context.Guild, $"Inventory for {Context.User}", null));
        }
    }
}
using System.Threading.Tasks;
using DiscordHackWeek.Interactive;
using DiscordHackWeek.Services.Combat;
using DiscordHackWeek.Services.Database;
using Qmmands;

namespace DiscordHackWeek.Modules
{
    public class GameModule : InteractiveBase
    {
        private readonly DbService _db;
        private readonly CombatHandling _combat;
        public GameModule(DbService db, CombatHandling combat)
        {
            _db = db;
            _combat = combat;
        }

        [Name("Search")]
        [Description("")]
        [Command("search")]
        public async Task PlayAsync()
        {

        }

        [Name("GoTo")]
        [Description("")]
        [Command("goto", "travel")]
        public async Task TravelAsync()
        {

        }

        [Name("Zones")]
        [Description("")]
        [Command("zones")]
        public async Task ZonesAsync()
        {

        }

        [Name("Zone")]
        [Description("")]
        [Command("zone")]
        public async Task ZoneAsync()
        {

        }

        [Name("Profile")]
        [Description("")]
        [Command("Profile")]
        public async Task ProfileAsync()
        {

        }

        [Name("Inventory")]
        [Description("")]
        [Command("inventory", "inv")]
        public async Task InventoryAsync()
        {

        }
    }
}
using DiscordHackWeek.Services.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace DiscordHackWeek.Services.Database
{
    public class DbService : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Dungeon> Dungeons { get; set; }
        public virtual DbSet<Continent> Continents { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
using DiscordHackWeek.Services.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace DiscordHackWeek.Services.Database
{
    public class DbService : DbContext
    {
        public DbService() { }
        public DbService(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Dungeon> Dungeons { get; set; }
        public virtual DbSet<Continent> Continents { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.HasKey(e => e.UserId);
                x.Property(e => e.UserId).HasConversion<long>();
            });
            modelBuilder.Entity<Dungeon>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Continent>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Zone>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Item>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Inventory>(x =>
            {
                x.HasKey(e => e.UserId);
                x.Property(e => e.UserId).HasConversion<long>();
            });
        }
    }
}
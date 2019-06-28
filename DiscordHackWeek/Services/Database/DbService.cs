using System;
using System.Linq;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Entities.Combat;
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
        public virtual DbSet<Enemy> Enemies { get; set; }

        public virtual DbSet<GuildConfig> GuildConfigs { get; set; }
        public virtual DbSet<IgnoreChannel> IgnoreChannels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
                optionsBuilder.UseNpgsql("Server=192.168.10.145;database=WumpusWonderland;Uid=postgres;Pwd=1023");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.HasKey(e => e.UserId);
                x.Property(e => e.UserId).HasConversion<long>();
                x.Property(e => e.AttackMode).HasConversion(
                    v => v.ToString(),
                    v => (AttackType)Enum.Parse(typeof(AttackType), v));
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
                x.HasData(new Continent
                {
                    Id = 1,
                    Name = "Cairian Kingdom"
                });
                x.HasData(new Continent
                {
                    Id = 2,
                    Name = "The Frenzied Country"
                });
                x.HasData(new Continent
                {
                    Id = 3,
                    Name = "Ethereal Rift"
                });
            });

            modelBuilder.Entity<Zone>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
                x.HasData(new Zone
                {
                    Id = 1,
                    Name = "Silver Willow Forest",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 2,
                    Name = "Quiet Forest",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 3,
                    Name = "Windy Hinterlands",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 4,
                    Name = "The Eastern Tundra",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 5,
                    Name = "Canomore Fjord",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 6,
                    Name = "Wild Forest",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 7,
                    Name = "Toxic Abyss",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 2
                });
                x.HasData(new Zone
                {
                    Id = 8,
                    Name = "Forbidden Badlands",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 2
                });
                x.HasData(new Zone
                {
                    Id = 9,
                    Name = "Raging Steppes",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 2
                });
                x.HasData(new Zone
                {
                    Id = 10,
                    Name = "Angry Steppes",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 2
                });
                x.HasData(new Zone
                {
                    Id = 11,
                    Name = "Phantom Lands",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 3
                });
                x.HasData(new Zone
                {
                    Id = 12,
                    Name = "Shadow Lands",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 3
                });
                x.HasData(new Zone
                {
                    Id = 13,
                    Name = "The Hidden Isles",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 3
                });
            });

            modelBuilder.Entity<Item>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
                x.Property(e => e.ItemType).HasConversion(
                    v => v.ToString(),
                    v => (ItemType)Enum.Parse(typeof(ItemType), v));
                // Regular armor
                x.HasData(new Item
                {
                    Id = 1,
                    Name = "Regular Weapon",
                    CritIncrease = 10,
                    DamageIncrease = 10,
                    HealthIncrease = 0,
                    Unique = false,
                    ItemType = ItemType.Weapon
                });
                x.HasData(new Item
                {
                    Id = 2,
                    Name = "Regular Armor",
                    CritIncrease = 0,
                    DamageIncrease = 0,
                    HealthIncrease = 20,
                    Unique = false,
                    ItemType = ItemType.Armor
                });
                // Food
                x.HasData(new Item
                {
                    Id = 3,
                    Name = "Bread",
                    CritIncrease = 0,
                    DamageIncrease = 0,
                    HealthIncrease = 20,
                    Unique = false,
                    ItemType = ItemType.Food
                });
                // Flask
                x.HasData(new Item
                {
                    Id = 4,
                    Name = "Damage Flask",
                    CritIncrease = 0,
                    DamageIncrease = 100,
                    HealthIncrease = 0,
                    Unique = false,
                    ItemType = ItemType.Flask
                });
                // Potion
                x.HasData(new Item
                {
                    Id = 5,
                    Name = "Health Potion",
                    CritIncrease = 0,
                    DamageIncrease = 0,
                    HealthIncrease = 30,
                    Unique = false,
                    ItemType = ItemType.Potion
                });
                // Iron armor
                x.HasData(new Item
                {
                    Id = 6,
                    Name = "Iron Weapon",
                    CritIncrease = 5,
                    DamageIncrease = 20,
                    HealthIncrease = 0,
                    Unique = false,
                    ItemType = ItemType.Weapon
                });
                x.HasData(new Item
                {
                    Id = 7,
                    Name = "Iron Armor",
                    CritIncrease = 5,
                    DamageIncrease = 0,
                    HealthIncrease = 20,
                    Unique = false,
                    ItemType = ItemType.Armor
                });
                // Steel armor
                x.HasData(new Item
                {
                    Id = 8,
                    Name = "Steel Weapon",
                    CritIncrease = 20,
                    DamageIncrease = 30,
                    HealthIncrease = 0,
                    Unique = false,
                    ItemType = ItemType.Weapon
                });
                x.HasData(new Item
                {
                    Id = 9,
                    Name = "Steel Armor",
                    CritIncrease = 10,
                    DamageIncrease = 0,
                    HealthIncrease = 30,
                    Unique = false,
                    ItemType = ItemType.Armor
                });
                // Trash items
                x.HasData(new Item
                {
                    Id = 10,
                    Name = "Spider Legs",
                    ItemType = ItemType.NoSpecial
                });
                x.HasData(new Item
                {
                    Id = 11,
                    Name = "Boar Head",
                    ItemType = ItemType.NoSpecial
                });
                x.HasData(new Item
                {
                    Id = 12,
                    Name = "Wolf Meat",
                    ItemType = ItemType.NoSpecial
                });
                x.HasData(new Item
                {
                    Id = 13,
                    Name = "Bear Meat",
                    ItemType = ItemType.NoSpecial
                });

                x.HasData(new Item
                {
                    Id = 14,
                    Name = "Broken Armor",
                    ItemType = ItemType.NoSpecial
                });
                x.HasData(new Item
                {
                    Id = 15,
                    Name = "Wool Cloth",
                    ItemType = ItemType.NoSpecial
                });
            });

            modelBuilder.Entity<Inventory>(x =>
            {
                x.HasKey(e => new { e.UserId, e.ItemId });
                x.Property(e => e.UserId).HasConversion<long>();
                x.Property(e => e.ItemType).HasConversion(
                    v => v.ToString(),
                    v => (ItemType)Enum.Parse(typeof(ItemType), v));
                // x.HasOne(e => e.Item).WithMany(e => e.UserInventories);
            });

            modelBuilder.Entity<Enemy>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
                x.Property(e => e.Type).HasConversion(
                    v => v.ToString(),
                    v => (EnemyType) Enum.Parse(typeof(EnemyType), v));

                // Silver Willow Forest monsters
                x.HasData(new Enemy
                {
                    Id = 1,
                    Name = "Bear",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 13 }
                });
                x.HasData(new Enemy
                {
                    Id = 2,
                    Name = "Boar",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 11 }
                });
                x.HasData(new Enemy
                {
                    Id = 3,
                    Name = "Spider",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 10 }
                });
                x.HasData(new Enemy
                {
                    Id = 4,
                    Name = "Wolf",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 12 }
                });
                x.HasData(new Enemy
                {
                    Id = 5,
                    Name = "Pirate",
                    Credit = 5,
                    Exp = 30,
                    Type = EnemyType.Humanoid,
                    Image = "",
                    ZoneId = 1,
                    ArmorId = 1,
                    WeaponId = 1,
                    LootTableIds = new [] { 14, 15 }
                });

                // Quiet Forest monsters
                x.HasData(new Enemy
                {
                    Id = 6,
                    Name = "Bear",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 13 }
                });
                x.HasData(new Enemy
                {
                    Id = 7,
                    Name = "Boar",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 11 }
                });
                x.HasData(new Enemy
                {
                    Id = 8,
                    Name = "Spider",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 10 }
                });
                x.HasData(new Enemy
                {
                    Id = 9,
                    Name = "Wolf",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 12 }
                });
                x.HasData(new Enemy
                {
                    Id = 10,
                    Name = "Pirate",
                    Credit = 5,
                    Exp = 50,
                    Type = EnemyType.Humanoid,
                    Image = "",
                    ZoneId = 2,
                    ArmorId = 1,
                    WeaponId = 1,
                    LootTableIds = new [] { 14, 15, 6, 7 }
                });
            });

            modelBuilder.Entity<GuildConfig>(x =>
            {
                x.HasKey(e => e.GuildId);
                x.Property(e => e.GuildId).HasConversion<long>();
            });

            modelBuilder.Entity<IgnoreChannel>(x =>
            {
                x.HasKey(e => e.GuildId);
                x.Property(e => e.IgnoreList).HasConversion(e => e.Select(i => (long)i).ToList(),
                    e => e.Select(i => (ulong)i).ToList());
            });
            
            /*
            modelBuilder.Entity<LootTable>(x =>
            {
                x.HasKey(e => new {e.EnemyId, e.ItemId}); 
                x.HasOne(bc => bc.Enemy)
                    .WithMany(b => b.Loot)
                    .HasForeignKey(bc => bc.EnemyId);
                x.HasOne(bc => bc.Item)
                    .WithMany(c => c.LootTable)
                    .HasForeignKey(bc => bc.ItemId);
            });
            */
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Entities.Combat;
using DiscordHackWeek.Services.Database.Tables;
using DiscordHackWeek.Services.Database.Tables.Mission;
using DiscordHackWeek.Services.Database.Tables.Objective;
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

        public virtual DbSet<Mission> Missions { get; set; }
        public virtual DbSet<MissionCompleted> MissionCompleted { get; set; }
        public virtual DbSet<MissionProgress> MissionProgress { get; set; }

        public virtual DbSet<Quest> Quests { get; set; }
        public virtual DbSet<CompletedQuest> CompletedQuests { get; set; }
        public virtual DbSet<ActiveQuest> ActiveQuests { get; set; }

        public virtual DbSet<GuildConfig> GuildConfigs { get; set; }
        public virtual DbSet<IgnoreChannel> IgnoreChannels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if  DEBUG
            DbWabWab.DbCon = "Server=192.168.10.145;database=WumpusWonderland2;Uid=postgres;Pwd=1023";
#endif
            if (!optionsBuilder.IsConfigured) 
                optionsBuilder.UseNpgsql(DbWabWab.DbCon);
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
                    Name = "The Eastern Tundra",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "The Eastern Tundra",
                    ImageSrc = "https://i.imgur.com/PMWHAnk.png",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 2,
                    Name = "Quiet Valley",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "Quiet Valley",
                    ImageSrc = "https://i.imgur.com/akHtZ70.png",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 3,
                    Name = "Canomore Fjord",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "Canomore Fjord",
                    ImageSrc = "https://i.imgur.com/WlHD4zF.png",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 4,
                    Name = "Silver Willow Forest",
                    LowLevel = 1,
                    HighLevel = 1,
                    Description = "",
                    ImageSrc = "",
                    ContinentId = 1
                });
                x.HasData(new Zone
                {
                    Id = 5,
                    Name = "Windy Hinterlands",
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
                    ItemType = ItemType.Weapon,
                    ImageUrl = "Data/ProfileAssets/Weapon/Sword.png"
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
                    ItemType = ItemType.Weapon,
                    ImageUrl = "Data/ProfileAssets/Weapon/Mace.png"
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
                    ItemType = ItemType.Weapon,
                    ImageUrl = "Data/ProfileAssets/Weapon/Axe.png"
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
                    Name = "Saber",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/gALvBbU.png",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 13 }
                });
                x.HasData(new Enemy
                {
                    Id = 2,
                    Name = "Wolf",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/9yaOrDg.png",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 11 }
                });
                x.HasData(new Enemy
                {
                    Id = 3,
                    Name = "Forest",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/vknNtaZ.png",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 10 }
                });
                x.HasData(new Enemy
                {
                    Id = 4,
                    Name = "Snake",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/4V82VgZ.png",
                    ZoneId = 1,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 12 }
                });
                x.HasData(new Enemy
                {
                    Id = 5,
                    Name = "Skeleton",
                    Credit = 5,
                    Exp = 30,
                    Type = EnemyType.Humanoid,
                    Image = "https://i.imgur.com/bHD4r1d.png",
                    ZoneId = 1,
                    ArmorId = 1,
                    WeaponId = 1,
                    LootTableIds = new [] { 14, 15 }
                });

                // Quiet Forest monsters
                x.HasData(new Enemy
                {
                    Id = 6,
                    Name = "Saber",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/gALvBbU.png",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 13 }
                });
                x.HasData(new Enemy
                {
                    Id = 7,
                    Name = "Wolf",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/9yaOrDg.png",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 11 }
                });
                x.HasData(new Enemy
                {
                    Id = 8,
                    Name = "Forest",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/vknNtaZ.png",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 10 }
                });
                x.HasData(new Enemy
                {
                    Id = 9,
                    Name = "Snake",
                    Credit = 0,
                    Exp = 20,
                    Type = EnemyType.Beast,
                    Image = "https://i.imgur.com/4V82VgZ.png",
                    ZoneId = 2,
                    ArmorId = null,
                    WeaponId = null,
                    LootTableIds = new [] { 12 }
                });
                x.HasData(new Enemy
                {
                    Id = 10,
                    Name = "Skeleton",
                    Credit = 5,
                    Exp = 50,
                    Type = EnemyType.Humanoid,
                    Image = "https://i.imgur.com/bHD4r1d.png",
                    ZoneId = 2,
                    ArmorId = 1,
                    WeaponId = 1,
                    LootTableIds = new [] { 14, 15, 6, 7 }
                });
            });

            modelBuilder.Entity<Mission>(x =>
            {
                x.HasKey(e => e.Id);
                x.HasData(new List<Mission>
                {
                    new Mission
                    {
                        Id = 1,
                        Name = "Patrol",
                        LevelRequirement = 1,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 10,
                        ExpReward = 100,
                        ActiveSpan = TimeSpan.FromHours(6),
                        Duration = TimeSpan.FromHours(2)
                    },
                    new Mission
                    {
                        Id = 2,
                        Name = "Pirate Cleanup",
                        LevelRequirement = 1,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 5,
                        ExpReward = 110,
                        ActiveSpan = TimeSpan.FromHours(12),
                        Duration = TimeSpan.FromHours(6)
                    },
                    new Mission
                    {
                        Id = 3,
                        Name = "Stockade Defense",
                        LevelRequirement = 1,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 10,
                        ExpReward = 100,
                        ActiveSpan = TimeSpan.FromHours(24),
                        Duration = TimeSpan.FromHours(12),
                        LootRewards = new []{ 1 }
                    },
                    new Mission
                    {
                        Id = 4,
                        Name = "Apple Gathering",
                        LevelRequirement = 1,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 10,
                        ExpReward = 100,
                        ActiveSpan = TimeSpan.FromHours(3),
                        Duration = TimeSpan.FromHours(1)
                    },
                    new Mission
                    {
                        Id = 5,
                        Name = "Lost Cat",
                        LevelRequirement = 1,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 10,
                        ExpReward = 50,
                        ActiveSpan = TimeSpan.FromHours(2),
                        Duration = TimeSpan.FromMinutes(30)
                    },
                    // Missions available from level 5
                    new Mission
                    {
                        Id = 6,
                        Name = "Night Shift",
                        LevelRequirement = 5,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 50,
                        ExpReward = 300,
                        ActiveSpan = TimeSpan.FromHours(12),
                        Duration = TimeSpan.FromHours(6)
                    },
                    new Mission
                    {
                        Id = 7,
                        Name = "Scout territory",
                        LevelRequirement = 5,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 50,
                        ExpReward = 1000,
                        ActiveSpan = TimeSpan.FromHours(24),
                        Duration = TimeSpan.FromHours(12)
                    },
                    new Mission
                    {
                        Id = 8,
                        Name = "Help out farmer",
                        LevelRequirement = 5,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 50,
                        ExpReward = 200,
                        ActiveSpan = TimeSpan.FromHours(2),
                        Duration = TimeSpan.FromMinutes(30)
                    },
                    new Mission
                    {
                        Id = 9,
                        Name = "Cook feast",
                        LevelRequirement = 5,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 50,
                        ExpReward = 300,
                        ActiveSpan = TimeSpan.FromHours(6),
                        Duration = TimeSpan.FromHours(2)
                    },
                    new Mission
                    {
                        Id = 10,
                        Name = "Patrol",
                        LevelRequirement = 5,
                        ActiveSince = DateTimeOffset.UtcNow,
                        Active = false,
                        CreditReward = 50,
                        ExpReward = 300,
                        ActiveSpan = TimeSpan.FromHours(6),
                        Duration = TimeSpan.FromHours(2)
                    }
                });
            });
            modelBuilder.Entity<MissionCompleted>(x =>
            {
                x.HasKey(e => new {e.UserId, e.MissionId});
                x.Property(e => e.UserId).HasConversion<long>();
            });
            modelBuilder.Entity<MissionProgress>(x =>
            {
                x.HasKey(e => new {e.UserId, e.MissionId});
                x.Property(e => e.UserId).HasConversion<long>();
            });

            modelBuilder.Entity<Quest>(x =>
            {
                x.HasKey(e => e.Id);
                x.HasData(new List<Quest>
                {
                    new Quest
                    {
                        Id = 1,
                        ZoneId = 1,
                        Amount = 5,
                        CreditReward = 10,
                        ExpReward = 100,
                        Name = "Saber Killer I",
                        EnemyId = 1
                    },
                    new Quest
                    {
                        Id = 2,
                        ZoneId = 1,
                        Amount = 5,
                        CreditReward = 10,
                        ExpReward = 100,
                        Name = "Wolf Killer I",
                        EnemyId = 2
                    },
                    new Quest
                    {
                        Id = 3,
                        ZoneId = 1,
                        Amount = 5,
                        CreditReward = 10,
                        ExpReward = 100,
                        Name = "Forest Killer I",
                        EnemyId = 3
                    },
                    new Quest
                    {
                        Id = 4,
                        ZoneId = 1,
                        Amount = 5,
                        CreditReward = 10,
                        ExpReward = 100,
                        Name = "Snake Killer I",
                        EnemyId = 4
                    },
                    new Quest
                    {
                        Id = 5,
                        ZoneId = 1,
                        Amount = 5,
                        CreditReward = 10,
                        ExpReward = 100,
                        Name = "Skeleton Killer I",
                        EnemyId = 5
                    },

                    new Quest
                    {
                        Id = 6,
                        ZoneId = 2,
                        Amount = 5,
                        CreditReward = 50,
                        ExpReward = 250,
                        Name = "Puma Killer II",
                        EnemyId = 6
                    },
                    new Quest
                    {
                        Id = 7,
                        ZoneId = 2,
                        Amount = 5,
                        CreditReward = 50,
                        ExpReward = 250,
                        Name = "Wolf Killer II",
                        EnemyId = 7
                    },
                    new Quest
                    {
                        Id = 8,
                        ZoneId = 2,
                        Amount = 5,
                        CreditReward = 50,
                        ExpReward = 250,
                        Name = "Forest Killer II",
                        EnemyId = 8
                    },
                    new Quest
                    {
                        Id = 9,
                        ZoneId = 2,
                        Amount = 5,
                        CreditReward = 50,
                        ExpReward = 250,
                        Name = "Snake Killer II",
                        EnemyId = 9
                    },
                    new Quest
                    {
                        Id = 10,
                        ZoneId = 2,
                        Amount = 5,
                        CreditReward = 50,
                        ExpReward = 250,
                        Name = "Skeleton Killer II",
                        EnemyId = 10
                    }
                });
            });
            modelBuilder.Entity<CompletedQuest>(x =>
            {
                x.HasKey(e => new { e.UserId, e.QuestId });
                x.Property(e => e.UserId).HasConversion<long>();
            });
            modelBuilder.Entity<ActiveQuest>(x =>
            {
                x.HasKey(e => new { e.UserId, e.QuestId });
                x.Property(e => e.UserId).HasConversion<long>();
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
        }
    }
}
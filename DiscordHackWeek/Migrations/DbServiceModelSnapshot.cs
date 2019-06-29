﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DiscordHackWeek.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DiscordHackWeek.Migrations
{
    [DbContext(typeof(DbService))]
    partial class DbServiceModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageSrc");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Continents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cairian Kingdom"
                        },
                        new
                        {
                            Id = 2,
                            Name = "The Frenzied Country"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ethereal Rift"
                        });
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Dungeon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("RequiredLevel");

                    b.HasKey("Id");

                    b.ToTable("Dungeons");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Enemy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArmorId");

                    b.Property<int>("Credit");

                    b.Property<int>("Exp");

                    b.Property<string>("HeavyAttack");

                    b.Property<string>("Image");

                    b.Property<string>("LightAttack");

                    b.Property<int[]>("LootTableIds");

                    b.Property<string>("Name");

                    b.Property<int>("RareDropChance");

                    b.Property<string>("ThumbImg");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<int?>("WeaponId");

                    b.Property<int>("ZoneId");

                    b.HasKey("Id");

                    b.ToTable("Enemies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/gALvBbU.png",
                            LootTableIds = new[] { 13 },
                            Name = "Saber",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 2,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/9yaOrDg.png",
                            LootTableIds = new[] { 11 },
                            Name = "Wolf",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 3,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/vknNtaZ.png",
                            LootTableIds = new[] { 10 },
                            Name = "Forest",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 4,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/4V82VgZ.png",
                            LootTableIds = new[] { 12 },
                            Name = "Snake",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 5,
                            ArmorId = 1,
                            Credit = 5,
                            Exp = 30,
                            Image = "https://i.imgur.com/bHD4r1d.png",
                            LootTableIds = new[] { 14, 15 },
                            Name = "Skeleton",
                            RareDropChance = 1,
                            Type = "Humanoid",
                            WeaponId = 1,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 6,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/gALvBbU.png",
                            LootTableIds = new[] { 13 },
                            Name = "Saber",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 7,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/9yaOrDg.png",
                            LootTableIds = new[] { 11 },
                            Name = "Wolf",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 8,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/vknNtaZ.png",
                            LootTableIds = new[] { 10 },
                            Name = "Forest",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 9,
                            Credit = 0,
                            Exp = 20,
                            Image = "https://i.imgur.com/4V82VgZ.png",
                            LootTableIds = new[] { 12 },
                            Name = "Snake",
                            RareDropChance = 1,
                            Type = "Beast",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 10,
                            ArmorId = 1,
                            Credit = 5,
                            Exp = 50,
                            Image = "https://i.imgur.com/bHD4r1d.png",
                            LootTableIds = new[] { 14, 15, 6, 7 },
                            Name = "Skeleton",
                            RareDropChance = 1,
                            Type = "Humanoid",
                            WeaponId = 1,
                            ZoneId = 2
                        });
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.GuildConfig", b =>
                {
                    b.Property<long>("GuildId")
                        .ValueGeneratedOnAdd();

                    b.Property<List<string>>("Prefixes");

                    b.HasKey("GuildId");

                    b.ToTable("GuildConfigs");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.IgnoreChannel", b =>
                {
                    b.Property<decimal>("GuildId")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<List<long>>("IgnoreList");

                    b.HasKey("GuildId");

                    b.ToTable("IgnoreChannels");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Inventory", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("ItemId");

                    b.Property<int>("Amount");

                    b.Property<string>("ItemType")
                        .IsRequired();

                    b.HasKey("UserId", "ItemId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CritIncrease");

                    b.Property<int>("DamageIncrease");

                    b.Property<int>("HealthIncrease");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("ItemType")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<bool>("Unique");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CritIncrease = 10,
                            DamageIncrease = 10,
                            HealthIncrease = 0,
                            ImageUrl = "Data/ProfileAssets/Weapon/Sword.png",
                            ItemType = "Weapon",
                            Name = "Regular Weapon",
                            Unique = false
                        },
                        new
                        {
                            Id = 2,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 20,
                            ItemType = "Armor",
                            Name = "Regular Armor",
                            Unique = false
                        },
                        new
                        {
                            Id = 3,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 20,
                            ItemType = "Food",
                            Name = "Bread",
                            Unique = false
                        },
                        new
                        {
                            Id = 4,
                            CritIncrease = 0,
                            DamageIncrease = 100,
                            HealthIncrease = 0,
                            ItemType = "Flask",
                            Name = "Damage Flask",
                            Unique = false
                        },
                        new
                        {
                            Id = 5,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 30,
                            ItemType = "Potion",
                            Name = "Health Potion",
                            Unique = false
                        },
                        new
                        {
                            Id = 6,
                            CritIncrease = 5,
                            DamageIncrease = 20,
                            HealthIncrease = 0,
                            ImageUrl = "Data/ProfileAssets/Weapon/Mace.png",
                            ItemType = "Weapon",
                            Name = "Iron Weapon",
                            Unique = false
                        },
                        new
                        {
                            Id = 7,
                            CritIncrease = 5,
                            DamageIncrease = 0,
                            HealthIncrease = 20,
                            ItemType = "Armor",
                            Name = "Iron Armor",
                            Unique = false
                        },
                        new
                        {
                            Id = 8,
                            CritIncrease = 20,
                            DamageIncrease = 30,
                            HealthIncrease = 0,
                            ImageUrl = "Data/ProfileAssets/Weapon/Axe.png",
                            ItemType = "Weapon",
                            Name = "Steel Weapon",
                            Unique = false
                        },
                        new
                        {
                            Id = 9,
                            CritIncrease = 10,
                            DamageIncrease = 0,
                            HealthIncrease = 30,
                            ItemType = "Armor",
                            Name = "Steel Armor",
                            Unique = false
                        },
                        new
                        {
                            Id = 10,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 0,
                            ItemType = "NoSpecial",
                            Name = "Spider Legs",
                            Unique = false
                        },
                        new
                        {
                            Id = 11,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 0,
                            ItemType = "NoSpecial",
                            Name = "Boar Head",
                            Unique = false
                        },
                        new
                        {
                            Id = 12,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 0,
                            ItemType = "NoSpecial",
                            Name = "Wolf Meat",
                            Unique = false
                        },
                        new
                        {
                            Id = 13,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 0,
                            ItemType = "NoSpecial",
                            Name = "Bear Meat",
                            Unique = false
                        },
                        new
                        {
                            Id = 14,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 0,
                            ItemType = "NoSpecial",
                            Name = "Broken Armor",
                            Unique = false
                        },
                        new
                        {
                            Id = 15,
                            CritIncrease = 0,
                            DamageIncrease = 0,
                            HealthIncrease = 0,
                            ItemType = "NoSpecial",
                            Name = "Wool Cloth",
                            Unique = false
                        });
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Mission.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTimeOffset>("ActiveSince");

                    b.Property<TimeSpan>("ActiveSpan");

                    b.Property<int>("CreditReward");

                    b.Property<TimeSpan>("Duration");

                    b.Property<int>("ExpReward");

                    b.Property<string[]>("FailedResponse");

                    b.Property<int>("LevelRequirement");

                    b.Property<int[]>("LootRewards");

                    b.Property<string>("Name");

                    b.Property<string[]>("SuccessfulResponse");

                    b.HasKey("Id");

                    b.ToTable("Missions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 880, DateTimeKind.Unspecified).AddTicks(8481), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 6, 0, 0, 0),
                            CreditReward = 10,
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            ExpReward = 100,
                            LevelRequirement = 1,
                            Name = "Patrol"
                        },
                        new
                        {
                            Id = 2,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(1548), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 12, 0, 0, 0),
                            CreditReward = 5,
                            Duration = new TimeSpan(0, 6, 0, 0, 0),
                            ExpReward = 110,
                            LevelRequirement = 1,
                            Name = "Pirate Cleanup"
                        },
                        new
                        {
                            Id = 3,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(1609), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(1, 0, 0, 0, 0),
                            CreditReward = 10,
                            Duration = new TimeSpan(0, 12, 0, 0, 0),
                            ExpReward = 100,
                            LevelRequirement = 1,
                            LootRewards = new[] { 1 },
                            Name = "Stockade Defense"
                        },
                        new
                        {
                            Id = 4,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2039), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 3, 0, 0, 0),
                            CreditReward = 10,
                            Duration = new TimeSpan(0, 1, 0, 0, 0),
                            ExpReward = 100,
                            LevelRequirement = 1,
                            Name = "Apple Gathering"
                        },
                        new
                        {
                            Id = 5,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2044), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 2, 0, 0, 0),
                            CreditReward = 10,
                            Duration = new TimeSpan(0, 0, 30, 0, 0),
                            ExpReward = 50,
                            LevelRequirement = 1,
                            Name = "Lost Cat"
                        },
                        new
                        {
                            Id = 6,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2071), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 12, 0, 0, 0),
                            CreditReward = 50,
                            Duration = new TimeSpan(0, 6, 0, 0, 0),
                            ExpReward = 300,
                            LevelRequirement = 5,
                            Name = "Night Shift"
                        },
                        new
                        {
                            Id = 7,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2073), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(1, 0, 0, 0, 0),
                            CreditReward = 50,
                            Duration = new TimeSpan(0, 12, 0, 0, 0),
                            ExpReward = 1000,
                            LevelRequirement = 5,
                            Name = "Scout territory"
                        },
                        new
                        {
                            Id = 8,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2075), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 2, 0, 0, 0),
                            CreditReward = 50,
                            Duration = new TimeSpan(0, 0, 30, 0, 0),
                            ExpReward = 200,
                            LevelRequirement = 5,
                            Name = "Help out farmer"
                        },
                        new
                        {
                            Id = 9,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2077), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 6, 0, 0, 0),
                            CreditReward = 50,
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            ExpReward = 300,
                            LevelRequirement = 5,
                            Name = "Cook feast"
                        },
                        new
                        {
                            Id = 10,
                            Active = false,
                            ActiveSince = new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2080), new TimeSpan(0, 0, 0, 0, 0)),
                            ActiveSpan = new TimeSpan(0, 6, 0, 0, 0),
                            CreditReward = 50,
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            ExpReward = 300,
                            LevelRequirement = 5,
                            Name = "Patrol"
                        });
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Mission.MissionCompleted", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("MissionId");

                    b.HasKey("UserId", "MissionId");

                    b.ToTable("MissionCompleted");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Mission.MissionProgress", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("MissionId");

                    b.Property<bool>("Completed");

                    b.Property<DateTimeOffset>("Started");

                    b.Property<bool>("Success");

                    b.Property<int>("SuccessChance");

                    b.HasKey("UserId", "MissionId");

                    b.ToTable("MissionProgress");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Objective.ActiveQuest", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("QuestId");

                    b.Property<int>("Progress");

                    b.HasKey("UserId", "QuestId");

                    b.ToTable("ActiveQuests");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Objective.CompletedQuest", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("QuestId");

                    b.HasKey("UserId", "QuestId");

                    b.ToTable("CompletedQuests");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Objective.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("CreditReward");

                    b.Property<int>("EnemyId");

                    b.Property<int>("ExpReward");

                    b.Property<int[]>("LootReward");

                    b.Property<string>("Name");

                    b.Property<int>("ZoneId");

                    b.HasKey("Id");

                    b.ToTable("Quests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 5,
                            CreditReward = 10,
                            EnemyId = 1,
                            ExpReward = 100,
                            Name = "Saber Killer I",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 5,
                            CreditReward = 10,
                            EnemyId = 2,
                            ExpReward = 100,
                            Name = "Wolf Killer I",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 5,
                            CreditReward = 10,
                            EnemyId = 3,
                            ExpReward = 100,
                            Name = "Forest Killer I",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 5,
                            CreditReward = 10,
                            EnemyId = 4,
                            ExpReward = 100,
                            Name = "Snake Killer I",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 5,
                            Amount = 5,
                            CreditReward = 10,
                            EnemyId = 5,
                            ExpReward = 100,
                            Name = "Skeleton Killer I",
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 6,
                            Amount = 5,
                            CreditReward = 50,
                            EnemyId = 6,
                            ExpReward = 250,
                            Name = "Puma Killer II",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 7,
                            Amount = 5,
                            CreditReward = 50,
                            EnemyId = 7,
                            ExpReward = 250,
                            Name = "Wolf Killer II",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 8,
                            Amount = 5,
                            CreditReward = 50,
                            EnemyId = 8,
                            ExpReward = 250,
                            Name = "Forest Killer II",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 9,
                            Amount = 5,
                            CreditReward = 50,
                            EnemyId = 9,
                            ExpReward = 250,
                            Name = "Snake Killer II",
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 10,
                            Amount = 5,
                            CreditReward = 50,
                            EnemyId = 10,
                            ExpReward = 250,
                            Name = "Skeleton Killer II",
                            ZoneId = 2
                        });
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArmorId");

                    b.Property<string>("AttackMode")
                        .IsRequired();

                    b.Property<int>("ContinentId");

                    b.Property<int>("Credit");

                    b.Property<int>("DamageTalent");

                    b.Property<int>("Exp");

                    b.Property<int>("HealthTalent");

                    b.Property<int>("Level");

                    b.Property<int>("TotalExp");

                    b.Property<int>("UnspentTalentPoints");

                    b.Property<int>("WeaponId");

                    b.Property<int>("ZoneId");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DiscordHackWeek.Services.Database.Tables.Zone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContinentId");

                    b.Property<string>("Description");

                    b.Property<int>("HighLevel");

                    b.Property<string>("ImageSrc");

                    b.Property<int>("LowLevel");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Zones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContinentId = 1,
                            Description = "The Eastern Tundra",
                            HighLevel = 1,
                            ImageSrc = "https://i.imgur.com/PMWHAnk.png",
                            LowLevel = 1,
                            Name = "The Eastern Tundra"
                        },
                        new
                        {
                            Id = 2,
                            ContinentId = 1,
                            Description = "Quiet Valley",
                            HighLevel = 1,
                            ImageSrc = "https://i.imgur.com/akHtZ70.png",
                            LowLevel = 1,
                            Name = "Quiet Valley"
                        },
                        new
                        {
                            Id = 3,
                            ContinentId = 1,
                            Description = "Canomore Fjord",
                            HighLevel = 1,
                            ImageSrc = "https://i.imgur.com/WlHD4zF.png",
                            LowLevel = 1,
                            Name = "Canomore Fjord"
                        },
                        new
                        {
                            Id = 4,
                            ContinentId = 1,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Silver Willow Forest"
                        },
                        new
                        {
                            Id = 5,
                            ContinentId = 1,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Windy Hinterlands"
                        },
                        new
                        {
                            Id = 6,
                            ContinentId = 1,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Wild Forest"
                        },
                        new
                        {
                            Id = 7,
                            ContinentId = 2,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Toxic Abyss"
                        },
                        new
                        {
                            Id = 8,
                            ContinentId = 2,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Forbidden Badlands"
                        },
                        new
                        {
                            Id = 9,
                            ContinentId = 2,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Raging Steppes"
                        },
                        new
                        {
                            Id = 10,
                            ContinentId = 2,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Angry Steppes"
                        },
                        new
                        {
                            Id = 11,
                            ContinentId = 3,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Phantom Lands"
                        },
                        new
                        {
                            Id = 12,
                            ContinentId = 3,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "Shadow Lands"
                        },
                        new
                        {
                            Id = 13,
                            ContinentId = 3,
                            Description = "",
                            HighLevel = 1,
                            ImageSrc = "",
                            LowLevel = 1,
                            Name = "The Hidden Isles"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

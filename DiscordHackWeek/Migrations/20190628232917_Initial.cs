using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DiscordHackWeek.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveQuests",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    QuestId = table.Column<int>(nullable: false),
                    Progress = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveQuests", x => new { x.UserId, x.QuestId });
                });

            migrationBuilder.CreateTable(
                name: "CompletedQuests",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    QuestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedQuests", x => new { x.UserId, x.QuestId });
                });

            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageSrc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dungeons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    RequiredLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dungeons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ZoneId = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    ThumbImg = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    LightAttack = table.Column<string>(nullable: true),
                    HeavyAttack = table.Column<string>(nullable: true),
                    Exp = table.Column<int>(nullable: false),
                    Credit = table.Column<int>(nullable: false),
                    LootTableIds = table.Column<int[]>(nullable: true),
                    RareDropChance = table.Column<int>(nullable: false),
                    WeaponId = table.Column<int>(nullable: true),
                    ArmorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildConfigs",
                columns: table => new
                {
                    GuildId = table.Column<long>(nullable: false),
                    Prefixes = table.Column<List<string>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildConfigs", x => x.GuildId);
                });

            migrationBuilder.CreateTable(
                name: "IgnoreChannels",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(nullable: false),
                    IgnoreList = table.Column<List<long>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoreChannels", x => x.GuildId);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    ItemType = table.Column<string>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => new { x.UserId, x.ItemId });
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Unique = table.Column<bool>(nullable: false),
                    ItemType = table.Column<string>(nullable: false),
                    HealthIncrease = table.Column<int>(nullable: false),
                    DamageIncrease = table.Column<int>(nullable: false),
                    CritIncrease = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionCompleted",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    MissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionCompleted", x => new { x.UserId, x.MissionId });
                });

            migrationBuilder.CreateTable(
                name: "MissionProgress",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    MissionId = table.Column<int>(nullable: false),
                    SuccessChance = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    Success = table.Column<bool>(nullable: false),
                    Started = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionProgress", x => new { x.UserId, x.MissionId });
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    LevelRequirement = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    CreditReward = table.Column<int>(nullable: false),
                    ExpReward = table.Column<int>(nullable: false),
                    LootRewards = table.Column<int[]>(nullable: true),
                    SuccessfulResponse = table.Column<string[]>(nullable: true),
                    FailedResponse = table.Column<string[]>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    ActiveSpan = table.Column<TimeSpan>(nullable: false),
                    ActiveSince = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ZoneId = table.Column<int>(nullable: false),
                    EnemyId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreditReward = table.Column<int>(nullable: false),
                    ExpReward = table.Column<int>(nullable: false),
                    LootReward = table.Column<int[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false),
                    TotalExp = table.Column<int>(nullable: false),
                    Credit = table.Column<int>(nullable: false),
                    AttackMode = table.Column<string>(nullable: false),
                    ContinentId = table.Column<int>(nullable: false),
                    ZoneId = table.Column<int>(nullable: false),
                    UnspentTalentPoints = table.Column<int>(nullable: false),
                    DamageTalent = table.Column<int>(nullable: false),
                    HealthTalent = table.Column<int>(nullable: false),
                    WeaponId = table.Column<int>(nullable: false),
                    ArmorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    LowLevel = table.Column<int>(nullable: false),
                    HighLevel = table.Column<int>(nullable: false),
                    ImageSrc = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ContinentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Continents",
                columns: new[] { "Id", "Description", "ImageSrc", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Cairian Kingdom" },
                    { 2, null, null, "The Frenzied Country" },
                    { 3, null, null, "Ethereal Rift" }
                });

            migrationBuilder.InsertData(
                table: "Enemies",
                columns: new[] { "Id", "ArmorId", "Credit", "Exp", "HeavyAttack", "Image", "LightAttack", "LootTableIds", "Name", "RareDropChance", "ThumbImg", "Type", "WeaponId", "ZoneId" },
                values: new object[,]
                {
                    { 10, 1, 5, 50, null, "https://i.imgur.com/bHD4r1d.png", null, new[] { 14, 15, 6, 7 }, "Skeleton", 1, null, "Humanoid", 1, 2 },
                    { 9, null, 0, 20, null, "https://i.imgur.com/4V82VgZ.png", null, new[] { 12 }, "Snake", 1, null, "Beast", null, 2 },
                    { 8, null, 0, 20, null, "https://i.imgur.com/vknNtaZ.png", null, new[] { 10 }, "Forest", 1, null, "Beast", null, 2 },
                    { 6, null, 0, 20, null, "https://i.imgur.com/gALvBbU.png", null, new[] { 13 }, "Saber", 1, null, "Beast", null, 2 },
                    { 7, null, 0, 20, null, "https://i.imgur.com/9yaOrDg.png", null, new[] { 11 }, "Wolf", 1, null, "Beast", null, 2 },
                    { 4, null, 0, 20, null, "https://i.imgur.com/4V82VgZ.png", null, new[] { 12 }, "Snake", 1, null, "Beast", null, 1 },
                    { 3, null, 0, 20, null, "https://i.imgur.com/vknNtaZ.png", null, new[] { 10 }, "Forest", 1, null, "Beast", null, 1 },
                    { 2, null, 0, 20, null, "https://i.imgur.com/9yaOrDg.png", null, new[] { 11 }, "Wolf", 1, null, "Beast", null, 1 },
                    { 1, null, 0, 20, null, "https://i.imgur.com/gALvBbU.png", null, new[] { 13 }, "Saber", 1, null, "Beast", null, 1 },
                    { 5, 1, 5, 30, null, "https://i.imgur.com/bHD4r1d.png", null, new[] { 14, 15 }, "Skeleton", 1, null, "Humanoid", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CritIncrease", "DamageIncrease", "HealthIncrease", "ImageUrl", "ItemType", "Name", "Unique" },
                values: new object[,]
                {
                    { 10, 0, 0, 0, null, "NoSpecial", "Spider Legs", false },
                    { 15, 0, 0, 0, null, "NoSpecial", "Wool Cloth", false },
                    { 14, 0, 0, 0, null, "NoSpecial", "Broken Armor", false },
                    { 13, 0, 0, 0, null, "NoSpecial", "Bear Meat", false },
                    { 12, 0, 0, 0, null, "NoSpecial", "Wolf Meat", false },
                    { 11, 0, 0, 0, null, "NoSpecial", "Boar Head", false },
                    { 9, 10, 0, 30, null, "Armor", "Steel Armor", false },
                    { 4, 0, 100, 0, null, "Flask", "Damage Flask", false },
                    { 7, 5, 0, 20, null, "Armor", "Iron Armor", false },
                    { 6, 5, 20, 0, "Data/ProfileAssets/Weapon/Mace.png", "Weapon", "Iron Weapon", false },
                    { 5, 0, 0, 30, null, "Potion", "Health Potion", false },
                    { 3, 0, 0, 20, null, "Food", "Bread", false },
                    { 2, 0, 0, 20, null, "Armor", "Regular Armor", false },
                    { 1, 10, 10, 0, "Data/ProfileAssets/Weapon/Sword.png", "Weapon", "Regular Weapon", false },
                    { 8, 20, 30, 0, "Data/ProfileAssets/Weapon/Axe.png", "Weapon", "Steel Weapon", false }
                });

            migrationBuilder.InsertData(
                table: "Missions",
                columns: new[] { "Id", "Active", "ActiveSince", "ActiveSpan", "CreditReward", "Duration", "ExpReward", "FailedResponse", "LevelRequirement", "LootRewards", "Name", "SuccessfulResponse" },
                values: new object[,]
                {
                    { 7, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2073), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(1, 0, 0, 0, 0), 50, new TimeSpan(0, 12, 0, 0, 0), 1000, null, 5, null, "Scout territory", null },
                    { 10, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2080), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 6, 0, 0, 0), 50, new TimeSpan(0, 2, 0, 0, 0), 300, null, 5, null, "Patrol", null },
                    { 9, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2077), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 6, 0, 0, 0), 50, new TimeSpan(0, 2, 0, 0, 0), 300, null, 5, null, "Cook feast", null },
                    { 8, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2075), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 2, 0, 0, 0), 50, new TimeSpan(0, 0, 30, 0, 0), 200, null, 5, null, "Help out farmer", null },
                    { 6, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2071), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 12, 0, 0, 0), 50, new TimeSpan(0, 6, 0, 0, 0), 300, null, 5, null, "Night Shift", null },
                    { 3, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(1609), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(1, 0, 0, 0, 0), 10, new TimeSpan(0, 12, 0, 0, 0), 100, null, 1, new[] { 1 }, "Stockade Defense", null },
                    { 4, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2039), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 3, 0, 0, 0), 10, new TimeSpan(0, 1, 0, 0, 0), 100, null, 1, null, "Apple Gathering", null },
                    { 5, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(2044), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 2, 0, 0, 0), 10, new TimeSpan(0, 0, 30, 0, 0), 50, null, 1, null, "Lost Cat", null },
                    { 2, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 881, DateTimeKind.Unspecified).AddTicks(1548), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 12, 0, 0, 0), 5, new TimeSpan(0, 6, 0, 0, 0), 110, null, 1, null, "Pirate Cleanup", null },
                    { 1, false, new DateTimeOffset(new DateTime(2019, 6, 28, 23, 29, 16, 880, DateTimeKind.Unspecified).AddTicks(8481), new TimeSpan(0, 0, 0, 0, 0)), new TimeSpan(0, 6, 0, 0, 0), 10, new TimeSpan(0, 2, 0, 0, 0), 100, null, 1, null, "Patrol", null }
                });

            migrationBuilder.InsertData(
                table: "Quests",
                columns: new[] { "Id", "Amount", "CreditReward", "EnemyId", "ExpReward", "LootReward", "Name", "ZoneId" },
                values: new object[,]
                {
                    { 9, 5, 50, 9, 250, null, "Snake Killer II", 2 },
                    { 8, 5, 50, 8, 250, null, "Forest Killer II", 2 },
                    { 7, 5, 50, 7, 250, null, "Wolf Killer II", 2 },
                    { 6, 5, 50, 6, 250, null, "Puma Killer II", 2 },
                    { 10, 5, 50, 10, 250, null, "Skeleton Killer II", 2 },
                    { 4, 5, 10, 4, 100, null, "Snake Killer I", 1 },
                    { 3, 5, 10, 3, 100, null, "Forest Killer I", 1 },
                    { 2, 5, 10, 2, 100, null, "Wolf Killer I", 1 },
                    { 1, 5, 10, 1, 100, null, "Saber Killer I", 1 },
                    { 5, 5, 10, 5, 100, null, "Skeleton Killer I", 1 }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "Id", "ContinentId", "Description", "HighLevel", "ImageSrc", "LowLevel", "Name" },
                values: new object[,]
                {
                    { 11, 3, "", 1, "", 1, "Phantom Lands" },
                    { 10, 2, "", 1, "", 1, "Angry Steppes" },
                    { 9, 2, "", 1, "", 1, "Raging Steppes" },
                    { 8, 2, "", 1, "", 1, "Forbidden Badlands" },
                    { 7, 2, "", 1, "", 1, "Toxic Abyss" },
                    { 4, 1, "", 1, "", 1, "Silver Willow Forest" },
                    { 5, 1, "", 1, "", 1, "Windy Hinterlands" },
                    { 3, 1, "Canomore Fjord", 1, "https://i.imgur.com/WlHD4zF.png", 1, "Canomore Fjord" },
                    { 2, 1, "Quiet Valley", 1, "https://i.imgur.com/akHtZ70.png", 1, "Quiet Valley" },
                    { 1, 1, "The Eastern Tundra", 1, "https://i.imgur.com/PMWHAnk.png", 1, "The Eastern Tundra" },
                    { 12, 3, "", 1, "", 1, "Shadow Lands" },
                    { 6, 1, "", 1, "", 1, "Wild Forest" },
                    { 13, 3, "", 1, "", 1, "The Hidden Isles" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveQuests");

            migrationBuilder.DropTable(
                name: "CompletedQuests");

            migrationBuilder.DropTable(
                name: "Continents");

            migrationBuilder.DropTable(
                name: "Dungeons");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropTable(
                name: "GuildConfigs");

            migrationBuilder.DropTable(
                name: "IgnoreChannels");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MissionCompleted");

            migrationBuilder.DropTable(
                name: "MissionProgress");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}

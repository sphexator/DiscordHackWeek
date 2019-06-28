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
                    CritIncrease = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
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
                columns: new[] { "Id", "ArmorId", "Credit", "Exp", "HeavyAttack", "Image", "LightAttack", "LootTableIds", "Name", "RareDropChance", "Type", "WeaponId", "ZoneId" },
                values: new object[,]
                {
                    { 10, 1, 5, 50, null, "", null, new[] { 14, 15, 6, 7 }, "Pirate", 1, "Humanoid", 1, 2 },
                    { 9, null, 0, 20, null, "", null, new[] { 12 }, "Wolf", 1, "Beast", null, 2 },
                    { 8, null, 0, 20, null, "", null, new[] { 10 }, "Spider", 1, "Beast", null, 2 },
                    { 6, null, 0, 20, null, "", null, new[] { 13 }, "Bear", 1, "Beast", null, 2 },
                    { 7, null, 0, 20, null, "", null, new[] { 11 }, "Boar", 1, "Beast", null, 2 },
                    { 4, null, 0, 20, null, "", null, new[] { 12 }, "Wolf", 1, "Beast", null, 1 },
                    { 3, null, 0, 20, null, "", null, new[] { 10 }, "Spider", 1, "Beast", null, 1 },
                    { 2, null, 0, 20, null, "", null, new[] { 11 }, "Boar", 1, "Beast", null, 1 },
                    { 1, null, 0, 20, null, "", null, new[] { 13 }, "Bear", 1, "Beast", null, 1 },
                    { 5, 1, 5, 30, null, "", null, new[] { 14, 15 }, "Pirate", 1, "Humanoid", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CritIncrease", "DamageIncrease", "HealthIncrease", "ItemType", "Name", "Unique" },
                values: new object[,]
                {
                    { 10, 0, 0, 0, "NoSpecial", "Spider Legs", false },
                    { 15, 0, 0, 0, "NoSpecial", "Wool Cloth", false },
                    { 14, 0, 0, 0, "NoSpecial", "Broken Armor", false },
                    { 13, 0, 0, 0, "NoSpecial", "Bear Meat", false },
                    { 12, 0, 0, 0, "NoSpecial", "Wolf Meat", false },
                    { 11, 0, 0, 0, "NoSpecial", "Boar Head", false },
                    { 9, 10, 0, 30, "Armor", "Steel Armor", false },
                    { 8, 20, 30, 0, "Weapon", "Steel Weapon", false },
                    { 7, 5, 0, 20, "Armor", "Iron Armor", false },
                    { 6, 5, 20, 0, "Weapon", "Iron Weapon", false },
                    { 5, 0, 0, 30, "Potion", "Health Potion", false },
                    { 4, 0, 100, 0, "Flask", "Damage Flask", false },
                    { 3, 0, 0, 20, "Food", "Bread", false },
                    { 2, 0, 0, 20, "Armor", "Regular Armor", false },
                    { 1, 10, 10, 0, "Weapon", "Regular Weapon", false }
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
                    { 3, 1, "", 1, "", 1, "Windy Hinterlands" },
                    { 5, 1, "", 1, "", 1, "Canomore Fjord" },
                    { 4, 1, "", 1, "", 1, "The Eastern Tundra" },
                    { 12, 3, "", 1, "", 1, "Shadow Lands" },
                    { 2, 1, "", 1, "", 1, "Quiet Forest" },
                    { 1, 1, "", 1, "", 1, "Silver Willow Forest" },
                    { 6, 1, "", 1, "", 1, "Wild Forest" },
                    { 13, 3, "", 1, "", 1, "The Hidden Isles" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Users");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}

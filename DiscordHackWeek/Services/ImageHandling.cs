using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Entities.Combat;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Database.Tables;
using DiscordHackWeek.Services.Experience;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace DiscordHackWeek.Services
{
    public class ImageHandling
    {
        private readonly FontFamily _arial;
        private readonly LevelHandling _level;

        private readonly FontCollection _fonts;
        private readonly Font _profileHeader;
        private readonly Font _profileText;

        private readonly Image<Rgba32> _profileBackground;
        private readonly Image<Rgba32> _profileTemplate;
        private readonly Image<Rgba32> _profileWumpus;
        private readonly Image<Rgba32> _profilePreText;

        public ImageHandling(LevelHandling level)
        {
            _level = level;
            _fonts = new FontCollection();
            _arial = _fonts.Install(@"Data/Font/ARIAL.TTF");

            _profileText = new Font(_arial, 12, FontStyle.Regular);
            _profileHeader = new Font(_arial, 45, FontStyle.Regular);

            _profileWumpus = Image.Load("Data/ProfileAssets/WumpusWonderlandProfile.png");
            _profileTemplate = Image.Load("Data/ProfileAssets/Frames.png");
            _profileBackground = Image.Load("Data/ProfileAssets/Background.jpg");
            _profilePreText = Image.Load("Data/ProfileAssets/Text.png");
        }

        public async Task<Stream> ProfileBuilder(SocketGuildUser socketUser, User user, CombatUser cbUser, DbService db)
        {
            var stream = new MemoryStream();
            using var img = new Image<Rgba32>(400, 400);
            var weaponItem = await db.Items.FindAsync(user.WeaponId);
            var armor = await db.Items.FindAsync(user.ArmorId);
            var zone = await db.Zones.FindAsync(user.ZoneId);
            var continent = await db.Continents.FindAsync(user.ContinentId);
            var progressBar = CreateProfileProgressBar(user);
            img.Mutate(x =>
            {
                x.DrawImage(_profileBackground, new Point(0, 0), GraphicsOptions.Default); // Background
                x.DrawImage(Image.Load(weaponItem.ImageUrl), new Point(0, 0), GraphicsOptions.Default); // Weapon
                x.DrawImage(_profileWumpus, new Point(0, 0), GraphicsOptions.Default); // Wumpus
                x.DrawImage(_profileTemplate, new Point(0, 0), GraphicsOptions.Default); // Shapes / Frame
                x.DrawImage(_profilePreText, new Point(0, 0), GraphicsOptions.Default);

                x.DrawText(new TextGraphicsOptions{ HorizontalAlignment = HorizontalAlignment.Center },  socketUser.Username, _profileHeader, Rgba32.White, new PointF(200, 12));
                x.DrawText($"{user.DamageTalent}", new Font(_arial, 15, FontStyle.Regular), Rgba32.White, new PointF(56, 153));
                x.DrawText($"{user.HealthTalent}", new Font(_arial, 15, FontStyle.Regular), Rgba32.White, new PointF(56, 185));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{user.Level}", new Font(_arial, 20, FontStyle.Regular), Rgba32.White, new PointF(40, 95));

                if (progressBar.Count >= 2) // TODO: Make the color scheme of the profile customizable
                    x.DrawLines(new GraphicsOptions(true), Rgba32.BlueViolet, 4, progressBar.ToArray());

                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{cbUser.Health}", new Font(_arial, 15, FontStyle.Regular), Rgba32.White, new PointF(348, 118));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{cbUser.AttackPower}", new Font(_arial, 20, FontStyle.Regular), Rgba32.White, new PointF(348, 156));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{cbUser.CriticalChance}%", new Font(_arial, 15, FontStyle.Regular), Rgba32.White, new PointF(348, 198));

                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{continent.Name}", new Font(_arial, 12, FontStyle.Regular), Rgba32.White, new PointF(99, 357));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{zone.Name}", new Font(_arial, 12, FontStyle.Regular), Rgba32.White, new PointF(99, 388));

                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{weaponItem.Name}", new Font(_arial, 12, FontStyle.Regular), Rgba32.White, new PointF(300, 357));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{armor.Name}", new Font(_arial, 12, FontStyle.Regular), Rgba32.White, new PointF(300, 388));
            }); // 348
            img.Save(stream, new PngEncoder());
            return stream;
        }

        private List<PointF> CreateProfileProgressBar(User userData)
        {
            var perc = userData.Exp / (float)_level.ExpToNextLevel(userData.Level);
            var numb = perc * 100 / 100 * 360 * 2;
            var points = new List<PointF>();
            const double radius = 22;

            for (var i = 0; i < numb; i++)
            {
                var radians = i * Math.PI / 360;

                var x = 40 + radius * Math.Cos(radians - Math.PI / 2);
                var y = 103 + radius * Math.Sin(radians - Math.PI / 2);
                points.Add(new PointF((float)x, (float)y));
            }

            return points;
        }
    }
}
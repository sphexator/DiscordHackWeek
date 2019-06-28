using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordHackWeek.Extensions;
using DiscordHackWeek.Entities.Combat;
using DiscordHackWeek.Services.Database;
using DiscordHackWeek.Services.Database.Tables;
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
        private readonly HttpClient _client;

        private readonly FontCollection _fonts;
        private readonly Font _profileHeader;

        private readonly Image<Rgba32> _profileBackground;
        private readonly Image<Rgba32> _profileTemplate;
        private readonly Image<Rgba32> _profileWumpus;
        private readonly Image<Rgba32> _profilePreText;
        private readonly Font _profileText;

        public ImageHandling(HttpClient client)
        {
            _client = client;

            _fonts = new FontCollection();
            _arial = _fonts.Install("Data/Font/ARIAL.TFF");

            _profileText = new Font(_arial, 12, FontStyle.Regular);
            _profileText = new Font(_arial, 45, FontStyle.Regular);

            _profileWumpus = Image.Load("Data/ProfileAssets/Wumpus.png");
            _profileTemplate = Image.Load("Data/ProfileAssets/Frames.png");
            _profileBackground = Image.Load("Data/ProfileAssets/Background.jpg");
            _profilePreText = Image.Load("Data/ProfileAssets/Text.png");
        }

        public async Task<Stream> ProfileBuilder(SocketGuildUser socketUser, User user, CombatUser cbUser, DbService db)
        {
            var stream = new MemoryStream();
            using var img = new Image<Rgba32>(400, 400);
            var weaponItem = await db.Items.FindAsync(user.WeaponId);
            img.Mutate(x =>
            {
                x.DrawImage(_profileBackground, new Point(0, 0), GraphicsOptions.Default); // Background
                x.DrawImage(Image.Load(weaponItem.ImageUrl), new Point(0, 0), GraphicsOptions.Default); // Weapon
                x.DrawImage(_profileWumpus, new Point(0, 0), GraphicsOptions.Default); // Wumpus
                x.DrawImage(_profileTemplate, new Point(0, 0), GraphicsOptions.Default); // Shapes / Frame

                x.DrawText(new TextGraphicsOptions{ HorizontalAlignment = HorizontalAlignment.Center },  socketUser.Username, _profileHeader, Rgba32.White, new PointF(200, 42));
                x.DrawText($"{user.DamageTalent}", _profileText, Rgba32.White, new PointF(56, 151));
                x.DrawText($"{user.HealthTalent}", _profileText, Rgba32.White, new PointF(56, 183));

                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{cbUser.Health}", _profileHeader, Rgba32.White, new PointF(348, 128));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{cbUser.AttackPower}", _profileHeader, Rgba32.White, new PointF(348, 171));
                x.DrawText(new TextGraphicsOptions { HorizontalAlignment = HorizontalAlignment.Center }, $"{cbUser.CriticalChance}%", _profileHeader, Rgba32.White, new PointF(348, 208));
            }); // 348
            img.Save(stream, new PngEncoder());
            return stream;
        }
    }
}
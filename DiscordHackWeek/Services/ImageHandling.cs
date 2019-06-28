using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordHackWeek.Extensions;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using Image = SixLabors.ImageSharp.Image;

namespace DiscordHackWeek.Services
{
    public class ImageHandling
    {
        private readonly FontFamily _arial;
        private readonly HttpClient _client;

        private readonly FontCollection _fonts;

        private readonly Image<Rgba32> _profileTemplate;
        private readonly FontFamily _times;

        public ImageHandling(HttpClient client)
        {
            _client = client;

            _fonts = new FontCollection();
            _times = _fonts.Install("Data/Fonts/TIMES.TTF");
            _arial = _fonts.Install("Data/Fonts/ARIAL.TFF");

            _profileTemplate = Image.Load("Data/Profile/Template.png");
        }

        private async Task<Image<Rgba32>> GetAvatarAsync(SocketUser user, Size size, int radius)
        {
            var response = await _client.GetStreamAsync(user.GetAvatar());
            using var img = Image.Load(response);
            var avi = img.CloneAndConvertToAvatarWithoutApply(size, radius);
            return avi.Clone();
        }

        private async Task<Image<Rgba32>> GetAvatarAsync(SocketUser user, Size size)
        {
            var response = await _client.GetStreamAsync(user.GetAvatar());
            using var img = Image.Load(response);
            img.Mutate(x => x.Resize(size));
            return img.Clone();
        }
    }
}
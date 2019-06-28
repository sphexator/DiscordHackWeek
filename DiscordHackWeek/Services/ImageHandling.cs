using System.Net.Http;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DiscordHackWeek.Services
{

    public class ImageHandling
    {
        private readonly HttpClient _client;

        private readonly FontCollection _fonts;
        private readonly FontFamily _times;
        private readonly FontFamily _arial;

        private readonly Image<Rgba32> _profileTemplate;

        public ImageHandling(HttpClient client)
        {
            _client = client;

            _fonts = new FontCollection();
            _times = _fonts.Install("Data/Fonts/TIMES.TTF");
            _arial = _fonts.Install("Data/Fonts/ARIAL.TFF");

            _profileTemplate = Image.Load("Data/Profile/Template.png");
        }
    }
}

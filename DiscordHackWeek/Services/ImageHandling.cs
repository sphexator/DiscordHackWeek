using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
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

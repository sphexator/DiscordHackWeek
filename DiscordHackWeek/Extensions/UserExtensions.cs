using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;

namespace DiscordHackWeek.Extensions
{
    public static class UserExtensions
    {
        public static string GetAvatar(this SocketUser user)
        {
            var avi = user.GetAvatarUrl(ImageFormat.Auto, 2048);
            if (avi != null) return avi;
            return user.GetDefaultAvatarUrl();
        }
    }
}

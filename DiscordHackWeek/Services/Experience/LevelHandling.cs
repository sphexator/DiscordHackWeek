using System.Threading.Tasks;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Services.Database.Tables;

namespace DiscordHackWeek.Services.Experience
{
    public class LevelHandling : INService
    {
        public int ExpToNextLevel(int level) => 10 * level * level + 200;

        public async Task AddExpAsync(int exp, User user)
        {
            if (user.Exp + exp >= ExpToNextLevel(user.Level))
            {
                user.Exp = user.Exp + exp - ExpToNextLevel(user.Level);
                user.Level++;

                if (user.Level % 2 == 0) user.UnspentTalentPoints++;
            }
            else user.Exp += exp;
            user.TotalExp += exp;
        }

        public async Task AddExpAndCreditAsync(int exp, int credit, User user)
        {
            await AddExpAsync(exp, user);
            user.Credit += credit;
        }
    }
}

using System.Threading.Tasks;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Services.Database.Tables;

namespace DiscordHackWeek.Services.Experience
{
    public class LevelHandling : INService
    {
        public int ExpToNextLevel(int level) => 10 * level * level + 200;

        public async Task AddExp(int exp, User user)
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

        public async Task AddExpAndCredit(int exp, int credit, User user)
        {
            await AddExp(exp, user);
            user.Credit += credit;
        }
    }
}

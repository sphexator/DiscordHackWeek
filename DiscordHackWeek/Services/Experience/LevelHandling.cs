using System.Threading.Tasks;
using DiscordHackWeek.Entities;
using DiscordHackWeek.Services.Database.Tables;

namespace DiscordHackWeek.Services.Experience
{
    public class LevelHandling : INService
    {
        public int ExpToNextLevel(int level) => 10 * level * level + 200;

        public bool AddExpAndCredit(int exp, int credit, User user, out string response)
        {
            if (user.Level != 30)
            {
                if (user.Exp + exp >= ExpToNextLevel(user.Level))
                {
                    user.Exp = user.Exp + exp - ExpToNextLevel(user.Level);
                    user.Level++;

                    if (user.Level % 2 == 0) user.UnspentTalentPoints++;

                    response = $"gained {exp} and leveled up to {user.Level}!";
                }
                else
                {
                    user.Exp += exp;
                    response = $"gained {exp}";
                }
                user.TotalExp += exp;
            }
            user.Credit += credit;
            response = null;
            return user.Level != 30;
        }
    }
}

using DiscordHackWeek.Entities;
using DiscordHackWeek.Services.Database.Tables;

namespace DiscordHackWeek.Services.Experience
{
    public class LevelHandling : INService
    {
        public int ExpToNextLevel(int level) => 10 * level * level + 200;

        public bool AddExpAndCredit(int exp, int credit, User user, out string response)
        {
            response = null;
            if (user.Level != 30)
            {
                if (user.Exp + exp >= ExpToNextLevel(user.Level))
                {
                    user.Exp = user.Exp + exp - ExpToNextLevel(user.Level);
                    user.Level++;

                    if (user.Level % 2 == 0) user.UnspentTalentPoints++;

                    response = $"gained {exp} exp and leveled up to {user.Level}!";
                }
                else
                {
                    user.Exp += exp;
                    response = $"gained {exp} exp";
                }
                user.TotalExp += exp;
            }
            user.Credit += credit;
            return user.Level != 30;
        }
    }
}

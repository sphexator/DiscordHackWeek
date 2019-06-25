namespace DiscordHackWeek.Entities.Combat
{
    public class CombatUser
    {
        public string Name { get; set; }

        public int Health { get; set; }
        public int DmgTaken { get; set; }

        public int AttackPower { get; set; }
        public int CriticalChance { get; set; }
    }
}
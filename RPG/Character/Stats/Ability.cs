namespace RPG
{
    public class Ability : Stat
    {
        public Ability(StatId statId) : base(statId) { }
        protected override int minPoints => 0;
        protected override int xpFactor => 3;
    }
}
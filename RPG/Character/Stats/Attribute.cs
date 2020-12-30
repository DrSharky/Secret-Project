namespace RPG
{
    public class Attribute : Stat
    {
        protected override int xpFactor => 4;
        public Attribute(StatId statId) : base(statId){}
    }
}
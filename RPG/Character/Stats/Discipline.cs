namespace RPG
{
    public class Discipline : Stat
    {
        public Discipline(StatId statId, string displayString) : base(statId)
        { displayName = displayString; }
        public override string displayName { get; protected set; }
        protected override int xpFactor => 5;
    }
}
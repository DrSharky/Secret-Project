namespace RPG
{
    public class Humanity : Stat
    {
        protected override int maxPoints => 10;
        protected override int minPoints => 3;
        protected override int xpFactor => 2;
        public Humanity()
        {
            id = StatId.Humanity;
        }
    }
}
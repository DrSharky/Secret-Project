namespace RPG
{
    public class Stat
    {
        #region Members
        public virtual string displayName { get => id.ToString(); protected set { displayName = value; } }
        protected StatId id { get; set; }
        protected int points { get; set; }
        protected virtual int maxPoints { get => 5; }
        protected virtual int minPoints { get => 1; }
        protected virtual int xpCost { get => points * xpFactor; }
        protected virtual int xpFactor { get; }
        protected StatType type { get; set; }
        #endregion

        #region Constructor
        public Stat() { }
        public Stat(StatId statId)
        {
            id = statId;
        }
        #endregion

        #region Methods
        protected int SetPoints(int val)
        {
            points = val;
            return points;
        }
        #endregion
    }
}

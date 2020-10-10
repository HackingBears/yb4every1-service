namespace HackingBears.GameService.Domain
{
    public sealed class Score
    {
        #region Properties

        public int Away { get; private set; }
        public int Home { get; private set; }

        #endregion

        #region Methods

        public void Reset()
        {
            Home = 0;
        }

        public void AddHome()
        {
            Home++;
        }

        public void AddAway()
        {
            Away++;
        }

        public override string ToString()
            => $"{Home}:{Away}";

        #endregion
    }
}
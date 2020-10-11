namespace HackingBears.GameService.Domain
{
    public class CountDown
    {
        #region Properties

        public int GameId { get; set; }

        public int SecondsToGameStart { get; set; }

        #endregion

        #region Constructor

        public CountDown()
        {
        }

        public CountDown(int gameId, int secondsToGameStart)
        {
            GameId = gameId;
            SecondsToGameStart = secondsToGameStart;
        }

        #endregion
    }
}
namespace HackingBears.GameService.Domain
{
    public sealed class GameEvent
    {
        #region Properties

        public int GameId { get; set; }

        public string Message { get; set; }

        #endregion

        #region Constructor

        public GameEvent()
        {
        }

        public GameEvent(int gameId, string message)
        {
            GameId = gameId;
            Message = message;
        }

        #endregion
    }
}
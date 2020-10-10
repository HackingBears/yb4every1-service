namespace HackingBears.GameService.Domain
{
    public sealed class Voting
    {
        #region Properties

        public int FrameNumber { get; set; }
        public GameAction GameAction { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string UserId { get; set; }

        #endregion
    }
}
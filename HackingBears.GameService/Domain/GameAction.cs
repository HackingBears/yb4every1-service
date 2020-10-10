namespace HackingBears.GameService.Domain
{
    public sealed class GameAction
    {
        #region Properties

        public Action Action { get; set; }

        public Direction Direction { get; set; }

        #endregion
    }
}
namespace HackingBears.GameService.Domain
{
    public sealed class GameAction
    {
        public Action Action { get; set; }
        public Direction Direction { get; set; }
    }
}
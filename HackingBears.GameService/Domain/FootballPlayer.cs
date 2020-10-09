namespace HackingBears.GameService.Domain
{
    public sealed class FootballPlayer
    {
        #region Properties

        public bool HasBall { get; set; }
        public int Id { get; set; }
        public Position Position { get; set; }

        #endregion
    }
}
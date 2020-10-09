namespace HackingBears.GameService.Domain
{
    public sealed class Game
    {
        #region Properties

        public int Id { get; set; }
        
        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        
        public string KickOff { get; set; }
        public string Stade { get; set; }

        #endregion
    }
}
using System.Collections.Generic;

namespace HackingBears.GameService.Domain
{
    public sealed class GameFrame
    {
        #region Properties

        public int GameId { get; set; }
        public int FrameExpiration { get; set; }
        public long FrameNumber { get; set; }
        public string GameEvent { get; set; }
        public string GameScore { get; set; }
        public string GameTime { get; set; }
        
        public Position Ball { get; set; }
        public List<FootballPlayer> Players { get; set; }

        #endregion
    }
}
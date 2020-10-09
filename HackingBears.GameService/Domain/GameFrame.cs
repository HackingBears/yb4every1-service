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

        public GameFrame Clone()
        {
            GameFrame frame = new GameFrame
            {
                GameId = GameId,
                FrameExpiration = FrameExpiration,
                FrameNumber = FrameNumber,
                GameEvent = GameEvent,
                GameScore = GameScore,
                GameTime = GameTime,
                Ball = Ball.Clone(),
                Players = new List<FootballPlayer>()
            };

            foreach (FootballPlayer player in Players)
            {
                frame.Players.Add(player.Clone());
            }

            return frame;
        }
    }
}
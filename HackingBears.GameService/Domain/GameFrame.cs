using System;
using System.Collections.Generic;

namespace HackingBears.GameService.Domain
{
    public sealed class GameFrame
    {
        public Guid GameId { get; set; }
        public long FrameNumber { get; set; }
        public int FrameExpiration { get; set; }
        public string GameEvent { get; set; }
        public string GameScore { get; set; }
        public string GameTime { get; set; }
        public List<Team> Teams { get; set; }
    }
}
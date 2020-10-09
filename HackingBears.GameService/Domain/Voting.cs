using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackingBears.GameService.Domain
{
    public sealed class Voting
    {

        #region Properties

        public string UserId { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
	    public GameAction GameAction { get; set; }
	    public int FrameNumber { get; set; }

        #endregion

    }
}

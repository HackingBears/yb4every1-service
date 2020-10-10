using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackingBears.GameService.Domain
{
    public sealed class VotingResult
    {

        #region Properties

        public int PlayerId { get; set; }

        public GameAction GameAction { get; set; }

        #endregion

    }
}

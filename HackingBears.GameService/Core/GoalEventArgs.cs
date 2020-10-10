using HackingBears.GameService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackingBears.GameService.Core
{
    public sealed class GoalEventArgs : EventArgs
        {
            #region Properties

            public int GameId { get; set; }

            public Team Team { get; set; }

        #endregion
    }

}

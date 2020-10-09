using System;

namespace HackingBears.GameService.Core
{
    public sealed class GameFinishedEventArgs : EventArgs
    {
        #region Properties

        public int GameId { get; set; }

        #endregion
    }
}
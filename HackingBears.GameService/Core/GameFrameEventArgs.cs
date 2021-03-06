using System;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public sealed class GameFrameEventArgs : EventArgs
    {
        #region Properties

        public GameFrame Frame { get; set; }

        public int GameId { get; set; }

        #endregion
    }
}
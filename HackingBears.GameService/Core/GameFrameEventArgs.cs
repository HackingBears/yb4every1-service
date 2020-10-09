using System;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public class GameFrameEventArgs : EventArgs
    {
        #region Properties

        public GameFrame Frame { get; set; }

        #endregion
    }
}
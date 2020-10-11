using System;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public sealed class GameCountDownEventArgs : EventArgs
    {
        #region Properties

        public CountDown CountDown { get; }

        #endregion

        #region Constructor

        public GameCountDownEventArgs(CountDown countDown)
        {
            CountDown = countDown;
        }

        #endregion
    }
}
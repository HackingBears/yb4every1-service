using System;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public sealed class GameEventEventArgs : EventArgs
    {
        #region Properties

        public GameEvent GameEvent { get; }

        #endregion

        #region Constructor

        public GameEventEventArgs(GameEvent gameEvent)
        {
            GameEvent = gameEvent;
        }

        #endregion
    }
}
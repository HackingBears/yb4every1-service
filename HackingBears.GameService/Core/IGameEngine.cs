using System;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public interface IGameEngine
    {
        #region Events

        event EventHandler<GameFrameEventArgs> OnFrameChanged;

        #endregion

        #region Properties

        GameState State { get; }

        #endregion

        #region Methods

        GameRegistration RegisterPlayer(in long userId);

        GameFrame GetCurrentFrame(Guid registrationGameId);

        void Start();

        #endregion

        void AddAction(GameAction action);
    }
}
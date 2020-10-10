using System;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public interface IGamePlayEngine
    {
        #region Events

        event EventHandler<GameFrameEventArgs> OnFrameChanged;
        event EventHandler<GameFinishedEventArgs> OnGameFinished;
        event EventHandler<GoalEventArgs> OnGoal;

        #endregion

        #region Properties

        GameStateDescription State { get; }

        #endregion

        #region Methods

        GameRegistration RegisterPlayer(in string userId, TeamType teamType);

        GameFrame GetCurrentFrame();

        void Start();

        void AddVoting(Voting voting);

        void GameFinished();

        #endregion
    }
}
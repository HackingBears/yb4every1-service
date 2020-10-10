using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public interface IGamePlayManager
    {
        #region Methods

        GameRegistration RegisterPlayer(in string userId, in int gameId, TeamType teamType);

        GameFrame GetCurrentFrame(int gameId);

        void Start(int gameId);

        void AddVoting(Voting voting);

        public void OpenGameForRegistration(int gameId);

        GameStateDescription GetGameState(in int gameId);

        #endregion
    }
}
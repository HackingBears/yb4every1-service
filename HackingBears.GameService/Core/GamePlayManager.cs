using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HackingBears.GameService.Domain;
using HackingBears.GameService.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HackingBears.GameService.Core
{
    public sealed class GamePlayManager : IGamePlayManager
    {
        #region Properties

        private Dictionary<int, IGamePlayEngine> GamePlayEngines { get; } = new Dictionary<int, IGamePlayEngine>();

        private IHubContext<GameHub, IGameHubAction> HubContext { get; }

        #endregion

        #region Constructor

        public GamePlayManager(IHubContext<GameHub, IGameHubAction> hubContext)
        {
            HubContext = hubContext;
        }

        #endregion

        #region Methods

        public void AddVoting(Voting voting)
        {
            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                GamePlayEngines[voting.GameId].AddVoting(voting);
            }
        }

        public void OpenGameForRegistration(int gameId)
        {
            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                // Prüfen ob es schon ein Spiel gibt
                if (GamePlayEngines.Keys.Contains(gameId))
                {
                    return;
                }

                // Neue GamePlay-Engine hinzufügen
                IGamePlayEngine gamePlayEngine = new GamePlayEngine(gameId);
                gamePlayEngine.OnFrameChanged += GamePlayEngine_OnFrameChanged;
                gamePlayEngine.OnGameFinished += GamePlayEngine_OnGameFinished;
                GamePlayEngines.Add(gameId, gamePlayEngine);
            }
        }

        public GameStateDescription GetGameState(in int gameId)
        {
            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                GamePlayEngines.TryGetValue(gameId, out IGamePlayEngine engine);
                return engine?.State ?? GameStateDescription.Undefined;
            }
        }

        private void GamePlayEngine_OnFrameChanged(object sender, GameFrameEventArgs e)
        {
            HubContext.Clients.All.UpdateGameFrame(e.Frame).Wait();
        }

        private void GamePlayEngine_OnGameFinished(object sender, GameFinishedEventArgs e)
        {
            int gameId = e.GameId;

            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                IGamePlayEngine gamePlayEngine = GamePlayEngines[gameId];
                gamePlayEngine.OnFrameChanged -= GamePlayEngine_OnFrameChanged;
                gamePlayEngine.OnGameFinished -= GamePlayEngine_OnGameFinished;
                GamePlayEngines.Remove(gameId);
            }

            HubContext.Clients.All.GameFinished().Wait();
        }

        public GameRegistration RegisterPlayer(in string userId, in int gameId, TeamType teamType)
        {
            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                return GamePlayEngines[gameId].RegisterPlayer(userId, teamType);
            }
        }

        public GameFrame GetCurrentFrame(int gameId)
        {
            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                return GamePlayEngines[gameId].GetCurrentFrame();
            }
        }

        public void Start(int gameId)
        {
            lock (((ICollection) GamePlayEngines).SyncRoot)
            {
                GamePlayEngines[gameId].Start();
            }
        }

        #endregion
    }
}
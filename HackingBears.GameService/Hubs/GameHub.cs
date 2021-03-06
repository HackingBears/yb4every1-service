using System;
using System.Text.Json;
using System.Threading.Tasks;
using HackingBears.GameService.Core;
using HackingBears.GameService.Domain;
using Microsoft.AspNetCore.SignalR;

namespace HackingBears.GameService.Hubs
{
    public sealed class GameHub : Hub<IGameHubAction>
    {
        #region Properties

        private IGamePlayManager GamePlayManager { get; }

        #endregion

        #region Constructor

        public GameHub(IGamePlayManager gamePlayManager)
        {
            GamePlayManager = gamePlayManager;
        }

        #endregion

        #region Methods

        public async Task RegisterToGame(int gameId, String teamType)
        {
            TeamType type = Enum.Parse<TeamType>(teamType);
            GameRegistration registration = GamePlayManager.RegisterPlayer(Context.ConnectionId, gameId, type);
            await Clients.Caller.CompleteRegistration(registration);

            GameFrame frame = GamePlayManager.GetCurrentFrame(gameId);
            await Clients.Caller.UpdateGameFrame(frame);
        }
        
        public Task VoteNextAction(Voting voting)
        {
            Console.WriteLine(JsonSerializer.Serialize(voting));
            GamePlayManager.AddVoting(voting);
            return Task.CompletedTask;
        }

        #endregion
    }
}
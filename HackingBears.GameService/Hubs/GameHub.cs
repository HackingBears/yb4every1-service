using System.Threading.Tasks;
using HackingBears.GameService.Core;
using HackingBears.GameService.Domain;
using Microsoft.AspNetCore.SignalR;

namespace HackingBears.GameService.Hubs
{
    public sealed class GameHub : Hub<IGameAction>
    {
        #region Properties

        private IGameEngine GameEngine { get; }

        #endregion

        #region Constructor

        public GameHub(IGameEngine gameEngine)
        {
            GameEngine = gameEngine;
            GameEngine.OnFrameChanged += OnGameFrameChanged;
        }

        #endregion

        #region Methods

        public async Task RegisterToGame(long userId)
        {
            GameRegistration registration = GameEngine.RegisterPlayer(userId);
            await Clients.Caller.CompleteRegistration(registration);

            GameFrame frame = GameEngine.GetCurrentFrame(registration.GameId);
            await Clients.Caller.UpdateGameFrame(frame);
        }

        public Task VoteNextAction(GameAction action)
        {
            GameEngine.AddAction(action);
            return Task.CompletedTask;
        }

        private void OnGameFrameChanged(object sender, GameFrameEventArgs args)
        {
            Clients.All.UpdateGameFrame(args.Frame).Wait();
        }

        #endregion
    }
}
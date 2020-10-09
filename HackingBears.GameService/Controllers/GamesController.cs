using HackingBears.GameService.Core;
using Microsoft.AspNetCore.Mvc;

namespace HackingBears.GameService.Controllers
{
    [ApiController]
    [Route("/app/games")]
    public sealed class GameController : ControllerBase
    {
        #region Properties

        private IGameEngine GameEngine { get; }

        #endregion

        #region Constructor

        public GameController(IGameEngine gameEngine)
        {
            GameEngine = gameEngine;
        }

        #endregion

        #region Methods

        [HttpPost]
        public ActionResult CreateGame()
        {
            switch (GameEngine.State)
            {
                case GameState.NotStarted:
                case GameState.Finished:
                    GameEngine.Start();
                    return Ok();
                default: return UnprocessableEntity("Game already started");
            }
        }

        #endregion
    }
}
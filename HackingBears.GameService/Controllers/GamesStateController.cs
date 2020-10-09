using HackingBears.GameService.Core;
using HackingBears.GameService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HackingBears.GameService.Controllers
{
    [ApiController]
    [Route("/app/games")]
    public sealed class GamesStateController : ControllerBase
    {
        #region Properties

        private IGamePlayManager GamePlayManager { get; }

        #endregion

        #region Constructor

        public GamesStateController(IGamePlayManager gamePlayManager)
        {
            GamePlayManager = gamePlayManager;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("{gameId}/state")]
        public IActionResult GetState(int gameId)
        {
            GameStateDescription stateDescription = GamePlayManager.GetGameState(gameId);
            return Ok(new GameState
                {
                    State = stateDescription
                }
            );
        }

        [HttpPut]
        [Route("{gameId}/state")]
        public IActionResult UpdateState(int gameId, GameState gameState)
        {
            GameStateDescription state = GamePlayManager.GetGameState(gameId);

            switch (state)
            {
                case GameStateDescription.Undefined when gameState.State == GameStateDescription.OpenForRegistration:
                    GamePlayManager.OpenGameForRegistration(gameId);
                    return NoContent();
                case GameStateDescription.OpenForRegistration when gameState.State == GameStateDescription.Start:
                    GamePlayManager.Start(gameId);
                    return NoContent();
                default: return UnprocessableEntity("Dieser Statuswechsel ist momentan nicht erlaubt");
            }
        }

        #endregion
    }
}
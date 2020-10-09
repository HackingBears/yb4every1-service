using System.Collections.Generic;
using HackingBears.GameService.DataAccess;
using HackingBears.GameService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HackingBears.GameService.Controllers
{
    [ApiController]
    [Route("/app/games")]
    public sealed class GameController : ControllerBase
    {
        #region Properties

        private IGameRepository GameRepository { get; }

        #endregion

        #region Constructor

        public GameController(IGameRepository gameRepository)
        {
            GameRepository = gameRepository;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ActionResult<List<Game>> GetGames()
            => Ok(GameRepository.GetGames());

        [HttpGet]
        [Route("{gameId}")]
        public ActionResult<Game> GetGamesById(int gameId)
        {
            Game game = GameRepository.GetGameByGameId(gameId);
            return game != null ? (ActionResult) Ok(game) : NotFound();
        }

        #endregion
    }
}
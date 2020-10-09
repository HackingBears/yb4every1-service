using System.Collections.Generic;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.DataAccess
{
    public interface IGameRepository
    {
        #region Methods

        List<Game> GetGames();

        Game GetGameByGameId(int id);

        #endregion
    }
}
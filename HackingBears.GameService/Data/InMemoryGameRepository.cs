using System.Collections.Generic;
using System.Linq;
using HackingBears.GameService.DataAccess;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Data
{
    public sealed class InMemoryGameRepository : IGameRepository
    {
        #region Properties

        private List<Game> Games { get; } = new List<Game>();

        #endregion

        #region Constructor

        public InMemoryGameRepository()
        {
            Games.Add(
                new Game
                {
                    Id = 1,
                    Stade = "Wankdorf",
                    AwayTeam = new Team
                    {
                        Id = 1,
                        Logo = "",
                        Name = "BSC Young Boys",
                        FirstColor = "#F7FE2E",
                        SecondColor = "#000000"
                    },
                    HomeTeam = new Team
                    {
                        Id = 2,
                        Logo = "",
                        Name = "FC Basel",
                        FirstColor = "#FF0000",
                        SecondColor = "#0000FF"
                    },
                    KickOff = "12:30"
                }
            );

            Games.Add(
                new Game
                {
                    Id = 2,
                    Stade = "Letzigrund",
                    AwayTeam = new Team
                    {
                        Id = 3,
                        Logo = "",
                        Name = "Fc Zürich",
                        FirstColor = "#FFFFFF",
                        SecondColor = "#0000FF"
                    },
                    HomeTeam = new Team
                    {
                        Id = 4,
                        Logo = "",
                        Name = "FC St. Gallen",
                        FirstColor = "#008000",
                        SecondColor = "#FFFFFF"
                    },
                    KickOff = "18:00"
                }
            );
        }

        #endregion

        #region Methods

        public List<Game> GetGames()
            => Games;

        public Game GetGameByGameId(int id)
            => Games.FirstOrDefault(game => game.Id == id);

        #endregion
    }
}
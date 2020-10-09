using System;

namespace HackingBears.GameService.Domain
{
    public class FootballField
    {
        #region Constants

        private const int MAX_X = 6;
        private const int MAX_Y = 3;
        private const int MIN_X = -6;
        private const int MIN_Y = -3;

        #endregion

        #region Fields

        private static readonly Random Random = new Random();

        #endregion

        #region Methods

        public static Position CreateRandomPosition()
            => new Position
            {
                X = Random.Next(MIN_X, MAX_X),
                Y = Random.Next(MIN_Y, MAX_Y)
            };

        #endregion
    }
}
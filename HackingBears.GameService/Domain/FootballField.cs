using System;

namespace HackingBears.GameService.Domain
{
    public class FootballField
    {
        private const int MIN_X = -6;
        private const int MAX_X = 6;
        private const int MIN_Y= -9;
        private const int MAX_Y= 9;
        private static Random Random = new Random();

        public static Position CreateRandomPosition()
            => new Position
            {
                X = Random.Next(MIN_X, MAX_X),
                Y = Random.Next(MIN_Y, MAX_Y)
            };
    }
}
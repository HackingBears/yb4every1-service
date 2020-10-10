namespace HackingBears.GameService.Domain
{
    public static class DirectionExtensions
    {
        public static Position ToPosition(this Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    return new Position
                    {
                        X = 0,
                        Y = -1,
                    };
                case Direction.NO:
                    return new Position
                    {
                        X = 1,
                        Y = -1,
                    };
                case Direction.O:
                    return new Position
                    {
                        X = 1,
                        Y = 0,
                    };
                case Direction.SO:
                    return new Position
                    {
                        X = 1,
                        Y = 1,
                    };
                case Direction.S:
                    return new Position
                    {
                        X = 0,
                        Y = 1,
                    };
                case Direction.SW:
                    return new Position
                    {
                        X = -1,
                        Y = 1,
                    };
                case Direction.W:
                    return new Position
                    {
                        X = -1,
                        Y = 0,
                    };
                case Direction.NW:
                    return new Position
                    {
                        X = -1,
                        Y = -1,
                    };
                default:
                    return new Position
                    {
                        X = 0,
                        Y = 0,
                    };
            }
        }
    }
}
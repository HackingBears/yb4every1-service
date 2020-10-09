namespace HackingBears.GameService.Domain
{
    public sealed class Position
    {
        #region Properties

        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #region Methods

        public Position Clone()
            => new Position
            {
                X = X,
                Y = Y
            };

        #endregion

        public static Position operator +(Position a, Position b)
            => new Position
            {
                X = (a?.X ?? 0) + (b?.X ?? 0),
                Y = (a?.Y ?? 0) + (b?.Y ?? 0)
            };

        public static Position operator *(Position a, int factor)
            => new Position
            {
                X = (a?.X ?? 0) * factor,
                Y = (a?.Y ?? 0) * factor
            };

        public static Position operator -(Position a, Position b)
            => new Position
            {
                X = (a?.X ?? 0) - (b?.X ?? 0),
                Y = (a?.Y ?? 0) - (b?.Y ?? 0)
            };
    }
}
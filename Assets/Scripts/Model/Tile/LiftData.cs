namespace Model.Tile
{
    public sealed class LiftData : TileData
    {
        public TileData To { get; }

        public LiftData(TileData from, TileData to) : base(from.Position, from.Color, CalculateFrom(from, to))
        {
            To = to;
        }

        private static Direction CalculateFrom(ITile from, ITile to)
        {
            if (from.Position.x > to.Position.x)
                return Direction.Left;

            if (from.Position.x < to.Position.x)
                return Direction.Right;

            return from.Position.z > to.Position.z ? Direction.Back : Direction.Forward;
        }
    }
}

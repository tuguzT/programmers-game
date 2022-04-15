using Model;

namespace Field
{
    public class Lift : Chunk
    {
        public Chunk To { get; }

        public Lift(Chunk from, Chunk to) : base(from.Position, from.Color, CalculateFrom(from, to))
        {
            To = to;
        }

        private static Direction CalculateFrom(Chunk from, Chunk to)
        {
            if (from.Position.x > to.Position.x)
                return Direction.Left;

            if (from.Position.x < to.Position.x)
                return Direction.Right;

            return from.Position.z > to.Position.z ? Direction.Back : Direction.Forward;
        }
    }
}

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
            if (to.Position.x < from.Position.x)
                return Direction.Left;

            if (to.Position.x > from.Position.x)
                return Direction.Right;

            return to.Position.z < from.Position.z ? Direction.Back : Direction.Forward;
        }
    }
}

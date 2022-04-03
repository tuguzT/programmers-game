using System;
using Random = UnityEngine.Random;

namespace Model
{
    public static class DirectionHelper
    {
        public static Direction GetRandom()
        {
            var values = Enum.GetValues(typeof(Direction));
            return (Direction)values.GetValue(Random.Range(0, values.Length));
        }

        public static Direction TurnAround(this Direction direction) => direction switch
        {
            Direction.Forward => Direction.Back,
            Direction.Back => Direction.Forward,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };

        public static Direction TurnLeft(this Direction direction) => direction switch
        {
            Direction.Forward => Direction.Left,
            Direction.Back => Direction.Right,
            Direction.Left => Direction.Back,
            Direction.Right => Direction.Forward,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };

        public static Direction TurnRight(this Direction direction) => direction switch
        {
            Direction.Forward => Direction.Right,
            Direction.Back => Direction.Left,
            Direction.Left => Direction.Forward,
            Direction.Right => Direction.Back,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }
}

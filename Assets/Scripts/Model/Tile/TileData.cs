using UnityEngine;

namespace Model.Tile
{
    public class TileData
    {
        public const float Width = 1.5f;
        public const float Height = 0.6f;

        public Vector3Int Position { get; }
        public IColor Color { get; }
        public Direction Direction { get; }

        public TileData(Vector3Int position, IColor color, Direction direction)
        {
            Position = position;
            Color = color;
            Direction = direction;
        }
    }
}

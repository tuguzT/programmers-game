using UnityEngine;

namespace Model.Tile
{
    public class TileData : ITile
    {
        public Vector3Int Position { get; }
        public Direction Direction { get; }
        public IColor Color { get; }

        public TileData(Vector3Int position, IColor color, Direction direction)
        {
            Position = position;
            Color = color;
            Direction = direction;
        }
    }
}

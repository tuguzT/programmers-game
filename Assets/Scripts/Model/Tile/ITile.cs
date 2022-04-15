using UnityEngine;

namespace Model.Tile
{
    public interface ITile
    {
        public const float Width = 1.5f;
        public const float Height = 0.6f;

        public Vector3Int Position { get; }
        public Direction Direction { get; }
    }
}

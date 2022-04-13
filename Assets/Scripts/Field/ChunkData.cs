using Model;
using UnityEngine;

namespace Field
{
    public readonly struct ChunkData
    {
        public const float Width = 1.5f;
        public const float Height = 0.6f;

        public readonly Vector3Int Position;
        public readonly IColor Color;
        public readonly Direction Direction;

        public ChunkData(Vector3Int position, IColor color, Direction direction)
        {
            Position = position;
            Color = color;
            Direction = direction;
        }
    }
}

using UnityEngine;

namespace Model
{
    public struct Chunk
    {
        public Vector3Int Position;
        public Color Color;

        public Chunk(Color color, Vector3Int position)
        {
            Color = color;
            Position = position;
        }
    }
}

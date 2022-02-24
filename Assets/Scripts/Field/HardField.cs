using Model;
using UnityEngine;

namespace Field
{
    internal sealed class HardField : IField
    {
        public Chunk[,] Generate()
        {
            var size = GameMode.Hard.FieldSize();
            var color = new Color(247 / 255f, 64 / 255f, 103 / 255f);

            var chunks = new Chunk[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    chunks[i, j] = new Chunk(color, position: new Vector3Int(i, 0, j));
                }
            }
            return chunks;
        }
    }
}

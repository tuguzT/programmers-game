using Model;
using UnityEngine;

namespace Field
{
    internal sealed class HardField : IField
    {
        public ChunkData[,] Generate()
        {
            var size = GameMode.Hard.FieldSize();
            var color = new Color(247 / 255f, 64 / 255f, 103 / 255f);

            var chunks = new ChunkData[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    chunks[i, j] = new ChunkData
                    {
                        color = color,
                        position = new Vector3Int(i, 0, j),
                    };
                }
            }
            return chunks;
        }
    }
}

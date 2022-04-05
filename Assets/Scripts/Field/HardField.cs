using Model;
using UnityEngine;

namespace Field
{
    internal sealed class HardField : IField
    {
        public uint Width => GameMode.Hard.FieldWidth();

        public ChunkData[,] Generate()
        {
            var color = new Color(247 / 255f, 64 / 255f, 103 / 255f);
            var chunks = new ChunkData[Width, Width];
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Width; j++)
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

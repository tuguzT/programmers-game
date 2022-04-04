using Model;
using UnityEngine;

namespace Field
{
    internal sealed class EasyField : IField
    {
        public ChunkData[,] Generate()
        {
            var size = GameMode.Easy.FieldWidth();
            var color = new Color(253 / 255f, 208 / 255f, 2 / 255f);

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

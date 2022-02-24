using Model;
using UnityEngine;

namespace Field
{
    internal sealed class EasyField : IField
    {
        public Chunk[,] Generate()
        {
            var size = GameMode.Easy.FieldSize();
            var color = new Color(253 / 255f, 208 / 255f, 2 / 255f);

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

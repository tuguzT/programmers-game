using Model;
using UnityEngine;

namespace Field
{
    internal sealed class EasyField : IField
    {
        public uint Width => GameMode.Easy.FieldWidth();

        public ChunkData[,] Generate()
        {
            var chunks = Init();
            Generate2X2(chunks);
            Generate2X4(chunks);
            return chunks;
        }

        private ChunkData[,] Init()
        {
            var color = new Color(253 / 255f, 208 / 255f, 2 / 255f);
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
        
        private void Generate2X2(ChunkData[,] chunks)
        {
            var color = new Color(2 / 255f, 168 / 255f, 112 / 255f);
            var x = Random.Range(0, (int)Width - 1);
            var y = Random.Range(0, (int)Width - 1);
            for (var i = x; i < x + 2; i++)
            {
                for (var j = y; j < y + 2; j++)
                {
                    chunks[i, j].position.y += 1;
                    chunks[i, j].color = color;
                }
            }
        }

        private void Generate2X4(ChunkData[,] chunks)
        {
            int length, width, x, y;
            var color = new Color(172 / 255f, 199 / 255f, 44 / 255f);
            bool found;
            do
            {
                found = true;
                length = Random.Range(0, 2) == 0 ? 2 : 4;
                width = 6 - length;
                x = Random.Range(0, (int)Width - length + 1);
                y = Random.Range(0, (int)Width - width + 1);
                for (var i = x; i < x + length; i++)
                {
                    for (var j = y; j < y + width; j++)
                    {
                        if (chunks[i, j].position.y != 0)
                        {
                            found = false;
                        }
                    }
                }
            } while (!found);

            for (var i = x; i < x + length; i++)
            {
                for (var j = y; j < y + width; j++)
                {
                    chunks[i, j].position.y += 1;
                    chunks[i, j].color = color;
                }
            }
        }
    }
}

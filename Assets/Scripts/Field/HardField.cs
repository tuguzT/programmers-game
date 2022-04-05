using System.Diagnostics.CodeAnalysis;
using Model;
using UnityEngine;

namespace Field
{
    internal sealed class HardField : IField
    {
        public uint Width => GameMode.Hard.FieldWidth();

        public ChunkData[,] Generate()
        {
            var chunks = Init();
            var blockData = Generate3X6X2(chunks);
            Generate3X6X1(chunks, blockData);
            Generate3X3(chunks);
            return chunks;
        }

        private ChunkData[,] Init()
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

        private (int, int, int, int) Generate3X6X2(ChunkData[,] chunks)
        {
            var color = new Color(230 / 255f, 96 / 255f, 201 / 255f);
            int x, y, length, width;
            do
            {
                length = Random.Range(0, 2) == 0 ? 6 : 3;
                width = 9 - length;
                x = Random.Range(0, (int)Width - length + 1);
                y = Random.Range(0, (int)Width - width + 1);
            } while (x != 0 && y != 0 && x != Width - length && y != Width - width);

            for (var i = x; i < x + length; i++)
            {
                for (var j = y; j < y + width; j++)
                {
                    chunks[i, j].position.y += 2;
                    chunks[i, j].color = color;
                }
            }
            return (x, y, length, width);
        }

        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        private void Generate3X6X1(ChunkData[,] chunks, (int, int, int, int) blockData)
        {
            (int, int) FirstCase(int length, int prevWidth, int prevX, int prevY)
            {
                var x = Random.Range(0, (int)Width - length + 1);
                int y;
                if (prevY < 3)
                    y = prevX == 0 || prevX == 3 ? prevY + prevWidth : 3;
                else if (prevY == 3)
                    y = Random.Range(0, 2) == 0 ? 0 : 6;
                else
                    y = prevX == 0 || prevX == 3 ? prevY - prevWidth : 3;

                return (x, y);
            }

            (int, int) SecondCase(int length, int width, int prevX, int prevY)
            {
                int x, y;
                switch (prevX)
                {
                    case 0:
                        y = Random.Range(0, (int)Width - width + 1);
                        x = prevY switch
                        {
                            0 => y == 6 ? Random.Range(0, (int)Width - length) : 3,
                            3 => y == 0 ? Random.Range(0, (int)Width - length) : 3,
                            _ => 3,
                        };
                        break;
                    case 6:
                        y = Random.Range(0, (int)Width - width + 1);
                        x = prevY switch
                        {
                            0 => y == 6 ? Random.Range(0, (int)Width - length) : 0,
                            3 => y == 0 ? Random.Range(0, (int)Width - length) : 0,
                            _ => 0,
                        };
                        break;
                    default:
                        x = Random.Range(0, (int)Width - length + 1);
                        y = prevY == 0 ? 6 : 0;
                        break;
                }

                return (x, y);
            }

            var color = new Color(246 / 255f, 151 / 255f, 85 / 255f);
            var (prevX, prevY, prevLength, prevWidth) = blockData;

            var length = Random.Range(0, 2) == 0 ? 6 : 3;
            var width = 9 - length;
            int x, y;
            if (length > width)
                if (prevLength > prevWidth)
                    (x, y) = FirstCase(length, prevWidth, prevX, prevY);
                else
                    (x, y) = SecondCase(length, width, prevX, prevY);
            else if (prevLength > prevWidth)
                (y, x) = SecondCase(width, length, prevY, prevX);
            else
                (y, x) = FirstCase(width, prevLength, prevY, prevX);

            for (var i = x; i < x + length; i++)
            {
                for (var j = y; j < y + width; j++)
                {
                    chunks[i, j].position.y += 1;
                    chunks[i, j].color = color;
                }
            }
        }

        private void Generate3X3(ChunkData[,] chunks)
        {
            var color = new Color(240 / 255f, 203 / 255f, 90 / 255f);

            int x, y;
            bool found;
            do
            {
                found = true;
                x = Random.Range(0, (int)Width - 2);
                y = Random.Range(0, (int)Width - 2);
                for (var i = x; i < x + 3; i++)
                {
                    for (var j = y; j < y + 3; j++)
                    {
                        if (chunks[i, j].position.y != 0)
                        {
                            found = false;
                        }
                    }
                }
            } while (!found);

            for (var i = x; i < x + 3; i++)
            {
                for (var j = y; j < y + 3; j++)
                { 
                    chunks[i, j].position.y += 1;
                    chunks[i, j].color = color;
                }
            }
        }
    }
}

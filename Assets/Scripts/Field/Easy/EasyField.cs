﻿using Model;
using UnityEngine;

namespace Field.Easy
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
            var chunks = new ChunkData[Width, Width];
            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Width; j++)
            {
                var position = new Vector3Int(i, 0, j);
                var direction = DirectionHelper.GetRandom();
                chunks[i, j] = new ChunkData(position, EasyColor.Yellow, direction);
            }

            return chunks;
        }

        private void Generate2X2(ChunkData[,] chunks)
        {
            var x = Random.Range(0, (int)Width - 1);
            var y = Random.Range(0, (int)Width - 1);
            for (var i = x; i < x + 2; i++)
            for (var j = y; j < y + 2; j++)
            {
                var chunk = chunks[i, j];
                var direction = chunk.Direction;
                var position = chunk.Position;
                position.y += 1;
                chunks[i, j] = new ChunkData(position, EasyColor.DarkGreen, direction);
            }
        }

        private void Generate2X4(ChunkData[,] chunks)
        {
            int length, width, x, y;
            bool found;
            do
            {
                found = true;
                length = Random.Range(0, 2) == 0 ? 2 : 4;
                width = 6 - length;
                x = Random.Range(0, (int)Width - length + 1);
                y = Random.Range(0, (int)Width - width + 1);
                for (var i = x; i < x + length; i++)
                for (var j = y; j < y + width; j++)
                    if (chunks[i, j].Position.y != 0)
                        found = false;
            } while (!found);

            for (var i = x; i < x + length; i++)
            for (var j = y; j < y + width; j++)
            {
                var chunk = chunks[i, j];
                var direction = chunk.Direction;
                var position = chunk.Position;
                position.y += 1;
                chunks[i, j] = new ChunkData(position, EasyColor.Green, direction);
            }
        }
    }
}

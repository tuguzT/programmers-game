using System;
using System.Linq;
using Model;
using Model.Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Field.Easy
{
    internal sealed class EasyFieldGenerator : IFieldGenerator
    {
        public uint Width => GameMode.Easy.FieldWidth();

        public (TileData[,], CarData[]) Generate()
        {
            var chunks = Init();
            Generate2X2(chunks);
            Generate2X4(chunks);
            var cars = GenerateBases(chunks);
            return (chunks, cars);
        }

        private TileData[,] Init()
        {
            var chunks = new TileData[Width, Width];
            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Width; j++)
            {
                var position = new Vector3Int(i, 0, j);
                var direction = DirectionHelper.GetRandom();
                chunks[i, j] = new TileData(position, EasyColor.Yellow, direction);
            }

            return chunks;
        }

        private void Generate2X2(TileData[,] chunks)
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
                chunks[i, j] = new TileData(position, EasyColor.DarkGreen, direction);
            }
        }

        private void Generate2X4(TileData[,] chunks)
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
                chunks[i, j] = new TileData(position, EasyColor.Green, direction);
            }
        }

        private CarData[] GenerateBases(TileData[,] chunks)
        {
            const GameMode gameMode = GameMode.Easy;

            var colors = Enum.GetValues(typeof(TeamColor))
                .Cast<TeamColor>()
                .OrderBy(_ => Random.Range(0f, 1f))
                .ToList();
            chunks[0, 0] = new BaseData(chunks[0, 0].Position, colors[0], gameMode);
            chunks[0, Width - 1] = new BaseData(chunks[0, Width - 1].Position, colors[1], gameMode);
            chunks[Width - 1, 0] = new BaseData(chunks[Width - 1, 0].Position, colors[2], gameMode);
            chunks[Width - 1, Width - 1] = new BaseData(chunks[Width - 1, Width - 1].Position, colors[3], gameMode);

            var cars = new CarData[]
            {
                new((BaseData)chunks[0, 0]),
                new((BaseData)chunks[0, Width - 1]),
                new((BaseData)chunks[Width - 1, 0]),
                new((BaseData)chunks[Width - 1, Width - 1])
            };
            return cars;
        }
    }
}

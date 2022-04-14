﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Field.Hard
{
    internal sealed class HardField : IField
    {
        public uint Width => GameMode.Hard.FieldWidth();

        public Chunk[,] Generate()
        {
            var chunks = Init();
            var blockData = Generate3X6X2(chunks);
            Generate3X6X1(chunks, blockData);
            Generate3X3(chunks);
            GenerateBases(chunks);
            return chunks;
        }

        private Chunk[,] Init()
        {
            var chunks = new Chunk[Width, Width];
            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Width; j++)
            {
                var position = new Vector3Int(i, 0, j);
                var direction = DirectionHelper.GetRandom();
                chunks[i, j] = new Chunk(position, HardColor.Red, direction);
            }

            return chunks;
        }

        private (int, int, int, int) Generate3X6X2(Chunk[,] chunks)
        {
            int x, y, length, width;
            do
            {
                length = Random.Range(0, 2) == 0 ? 6 : 3;
                width = 9 - length;
                x = Random.Range(0, (int)Width - length + 1);
                y = Random.Range(0, (int)Width - width + 1);
            } while (x != 0 && y != 0 && x != Width - length && y != Width - width);

            for (var i = x; i < x + length; i++)
            for (var j = y; j < y + width; j++)
            {
                var chunk = chunks[i, j];
                var direction = chunk.Direction;
                var position = chunk.Position;
                position.y += 2;
                chunks[i, j] = new Chunk(position, HardColor.Pink, direction);
            }

            return (x, y, length, width);
        }

        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        private void Generate3X6X1(Chunk[,] chunks, (int, int, int, int) blockData)
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
                            _ => 3
                        };
                        break;
                    case 6:
                        y = Random.Range(0, (int)Width - width + 1);
                        x = prevY switch
                        {
                            0 => y == 6 ? Random.Range(0, (int)Width - length) : 0,
                            3 => y == 0 ? Random.Range(0, (int)Width - length) : 0,
                            _ => 0
                        };
                        break;
                    default:
                        x = Random.Range(0, (int)Width - length + 1);
                        y = prevY == 0 ? 6 : 0;
                        break;
                }

                return (x, y);
            }

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
            for (var j = y; j < y + width; j++)
            {
                var chunk = chunks[i, j];
                var direction = chunk.Direction;
                var position = chunk.Position;
                position.y += 1;
                chunks[i, j] = new Chunk(position, HardColor.Orange, direction);
            }
        }

        private void Generate3X3(Chunk[,] chunks)
        {
            int x, y;
            bool found;
            do
            {
                found = true;
                x = Random.Range(0, (int)Width - 2);
                y = Random.Range(0, (int)Width - 2);
                for (var i = x; i < x + 3; i++)
                for (var j = y; j < y + 3; j++)
                    if (chunks[i, j].Position.y != 0)
                        found = false;
            } while (!found);

            for (var i = x; i < x + 3; i++)
            for (var j = y; j < y + 3; j++)
            {
                var chunk = chunks[i, j];
                var direction = chunk.Direction;
                var position = chunk.Position;
                position.y += 1;
                chunks[i, j] = new Chunk(position, HardColor.Yellow, direction);
            }
        }

        private void GenerateBases(Chunk[,] chunks)
        {
            const GameMode gameMode = GameMode.Hard;

            var colors = Enum.GetValues(typeof(Car.Color))
                .Cast<Car.Color>()
                .OrderBy(_ => Random.Range(0f, 1f))
                .ToList();
            chunks[0, 0] = new Base(chunks[0, 0].Position, colors[0], gameMode);
            chunks[0, Width - 1] = new Base(chunks[0, Width - 1].Position, colors[1], gameMode);
            chunks[Width - 1, 0] = new Base(chunks[Width - 1, 0].Position, colors[2], gameMode);
            chunks[Width - 1, Width - 1] = new Base(chunks[Width - 1, Width - 1].Position, colors[3], gameMode);
        }
    }
}
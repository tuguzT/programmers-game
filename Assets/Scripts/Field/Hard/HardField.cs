using System;
using System.Collections.Immutable;
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
            var dataFirst = Generate3X6X2(chunks);
            var dataSecond = Generate3X6X1(chunks, dataFirst);
            var dataThird = Generate3X3(chunks);
            GenerateBases(chunks);
            for (var i = 0; i < 2; i++)
            {
                GenerateLift(chunks, dataFirst);
                GenerateLift(chunks, dataSecond);
                GenerateLift(chunks, dataThird);
            }

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
        private (int, int, int, int) Generate3X6X1(Chunk[,] chunks, (int, int, int, int) data)
        {
            (int, int) FirstCase(int length, int prevWidth, int prevX, int prevY)
            {
                var x = Random.Range(0, (int)Width - length + 1);
                var y = prevY switch
                {
                    < 3 => prevX is 0 or 3 ? prevY + prevWidth : 3,
                    3 => Random.Range(0, 2) == 0 ? 0 : 6,
                    _ => prevX is 0 or 3 ? prevY - prevWidth : 3
                };

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

            var (prevX, prevY, prevLength, prevWidth) = data;

            var length = Random.Range(0, 2) == 0 ? 6 : 3;
            var width = 9 - length;
            int x, y;
            if (length > width)
                (x, y) = prevLength > prevWidth
                    ? FirstCase(length, prevWidth, prevX, prevY)
                    : SecondCase(length, width, prevX, prevY);
            else
                (y, x) = prevLength > prevWidth
                    ? SecondCase(width, length, prevY, prevX)
                    : FirstCase(width, prevLength, prevY, prevX);

            for (var i = x; i < x + length; i++)
            for (var j = y; j < y + width; j++)
            {
                var chunk = chunks[i, j];
                var direction = chunk.Direction;
                var position = chunk.Position;
                position.y += 1;
                chunks[i, j] = new Chunk(position, HardColor.Orange, direction);
            }

            return (x, y, length, width);
        }

        private (int, int, int, int) Generate3X3(Chunk[,] chunks)
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

            return (x, y, 3, 3);
        }

        private void GenerateBases(Chunk[,] chunks)
        {
            const GameMode gameMode = GameMode.Hard;

            var colors = Enum.GetValues(typeof(Car.Color))
                .Cast<Car.Color>()
                .OrderBy(_ => Random.Range(0f, 1f))
                .ToImmutableList();
            chunks[0, 0] = new Base(chunks[0, 0].Position, colors[0], gameMode);
            chunks[0, Width - 1] = new Base(chunks[0, Width - 1].Position, colors[1], gameMode);
            chunks[Width - 1, 0] = new Base(chunks[Width - 1, 0].Position, colors[2], gameMode);
            chunks[Width - 1, Width - 1] = new Base(chunks[Width - 1, Width - 1].Position, colors[3], gameMode);
        }

        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        private void GenerateLift(Chunk[,] chunks, (int, int, int, int) data)
        {
            const int attempts = 10;

            var (x, y, length, width) = data;

            bool Generated(Direction direction)
            {
                var remaining = 0;
                int range;

                switch (direction)
                {
                    case Direction.Forward:
                        if (y + width >= Width) return false;

                        for (var k = x; k < x + length; k++)
                            if (chunks[k, y + width - 1] is Lift)
                                return false;
                        do
                        {
                            if (++remaining > attempts)
                                return false;
                            range = Random.Range(0, length) + x;
                        } while (chunks[range, y + width - 1].Position.y <= chunks[range, y + width].Position.y
                                 || chunks[range, y + width] is Lift
                                 || chunks[range, y + width - 1] is Lift
                                 || (y + width == 8 && range is 8 or 0));

                        chunks[range, y + width - 1] = new Lift(
                            chunks[range, y + width - 1],
                            chunks[range, y + width]
                        );
                        return true;
                    case Direction.Back:
                        if (y <= 0) return false;

                        for (var k = x; k < x + length; k++)
                            if (chunks[k, y] is Lift)
                                return false;
                        do
                        {
                            if (++remaining > attempts)
                                return false;
                            range = Random.Range(0, length) + x;
                        } while (chunks[range, y].Position.y <= chunks[range, y - 1].Position.y
                                 || chunks[range, y - 1] is Lift
                                 || chunks[range, y] is Lift
                                 || (y == 1 && range is 8 or 0));

                        chunks[range, y] = new Lift(
                            chunks[range, y],
                            chunks[range, y - 1]
                        );
                        return true;
                    case Direction.Left:
                        if (x + length >= Width) return false;

                        for (var k = y; k < y + width; k++)
                            if (chunks[x + length - 1, k] is Lift)
                                return false;
                        do
                        {
                            if (++remaining > attempts)
                                return false;
                            range = Random.Range(0, width) + y;
                        } while (chunks[x + length - 1, range].Position.y <= chunks[x + length, range].Position.y
                                 || chunks[x + length, range] is Lift
                                 || chunks[x + length - 1, range] is Lift
                                 || (x + length == 8 && range is 8 or 0));

                        chunks[x + length - 1, range] = new Lift(
                            chunks[x + length - 1, range],
                            chunks[x + length, range]
                        );
                        return true;
                    case Direction.Right:
                        if (x <= 0) return false;

                        for (var k = y; k < y + width; k++)
                            if (chunks[x, k] is Lift)
                                return false;
                        do
                        {
                            if (++remaining > attempts)
                                return false;
                            range = Random.Range(0, width) + y;
                        } while (chunks[x, range].Position.y <= chunks[x - 1, range].Position.y
                                 || chunks[x - 1, range] is Lift
                                 || chunks[x, range] is Lift
                                 || (x == 1 && range is 8 or 0));

                        chunks[x, range] = new Lift(
                            chunks[x, range],
                            chunks[x - 1, range]
                        );
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }

            Direction direction;
            var i = 0;
            do
            {
                direction = DirectionHelper.GetRandom();
            } while (!Generated(direction) && i++ < attempts);
        }
    }
}

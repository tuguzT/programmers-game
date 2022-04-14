using System;
using System.Collections.Immutable;
using Model;
using UnityEngine;
using Color = Car.Color;

namespace Field
{
    public class Base : Chunk
    {
        public Color CarColor { get; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ImmutableHashSet<Color> LabColors { get; }

        public Base(Vector3Int position, Color carColor, GameMode gameMode) : base(position, BaseColor.Instance,
            Direction.Forward)
        {
            LabColors = gameMode switch
            {
                GameMode.Easy => ImmutableHashSet.Create(Car.Color.Yellow, Car.Color.Blue, Car.Color.Green,
                    Car.Color.Red),
                GameMode.Hard => carColor switch
                {
                    Car.Color.Red => ImmutableHashSet.Create(Car.Color.Blue, Car.Color.Yellow),
                    Car.Color.Green => ImmutableHashSet.Create(Car.Color.Red, Car.Color.Yellow),
                    Car.Color.Yellow => ImmutableHashSet.Create(Car.Color.Blue, Car.Color.Green),
                    Car.Color.Blue => ImmutableHashSet.Create(Car.Color.Red, Car.Color.Green),
                    _ => throw new ArgumentOutOfRangeException(nameof(carColor), carColor, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
            CarColor = carColor;
        }
    }
}

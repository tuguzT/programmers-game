using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using Color = Car.Color;

namespace Field
{
    public class Base : Chunk
    {
        public readonly Color CarColor;
        public readonly HashSet<Color> LabColors;

        public Base(Vector3Int position, Color carColor, GameMode gameMode) : base(position, new BaseColor(),
            Direction.Forward)
        {
            LabColors = gameMode switch
            {
                GameMode.Easy => new HashSet<Color>
                {
                    Car.Color.Yellow, Car.Color.Blue, Car.Color.Green, Car.Color.Red
                },
                GameMode.Hard => carColor switch
                {
                    Car.Color.Red => new HashSet<Color> { Car.Color.Blue, Car.Color.Yellow },
                    Car.Color.Green => new HashSet<Color> { Car.Color.Red, Car.Color.Yellow },
                    Car.Color.Yellow => new HashSet<Color> { Car.Color.Blue, Car.Color.Green },
                    Car.Color.Blue => new HashSet<Color> { Car.Color.Red, Car.Color.Green },
                    _ => throw new ArgumentOutOfRangeException(nameof(carColor), carColor, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
            CarColor = carColor;
        }
    }
}

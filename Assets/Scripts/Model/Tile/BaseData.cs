using System;
using System.Collections.Immutable;
using System.Linq;
using Field;
using UnityEngine;

namespace Model.Tile
{
    public sealed class BaseData : TileData
    {
        public TeamColor TeamColor { get; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ImmutableHashSet<TeamColor> LabColors { get; }

        public BaseData(Vector3Int position, TeamColor teamColor, Difficulty difficulty) : base(position, BaseColor.Instance, Direction.Forward)
        {
            LabColors = difficulty switch
            {
                Difficulty.Easy => Enum.GetValues(typeof(TeamColor)).Cast<TeamColor>().ToImmutableHashSet(),
                Difficulty.Hard => teamColor switch
                {
                    TeamColor.Red => ImmutableHashSet.Create(TeamColor.Blue, TeamColor.Yellow),
                    TeamColor.Green => ImmutableHashSet.Create(TeamColor.Red, TeamColor.Yellow),
                    TeamColor.Yellow => ImmutableHashSet.Create(TeamColor.Blue, TeamColor.Green),
                    TeamColor.Blue => ImmutableHashSet.Create(TeamColor.Red, TeamColor.Green),
                    _ => throw new ArgumentOutOfRangeException(nameof(teamColor), teamColor, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
            TeamColor = teamColor;
        }
    }
}

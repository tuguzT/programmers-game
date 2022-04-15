using System;
using System.Diagnostics.CodeAnalysis;
using Attributes;
using Model;
using Model.Tile;
using UnityEngine;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public class Car : AbstractTile
{
    [field: SerializeField]
    [field: ReadOnly]
    public TeamColor TeamColor { get; private set; }

    public override void FromData(ITile tile)
    {
        base.FromData(tile);
        if (tile is CarData carData)
        {
            TeamColor = carData.TeamColor;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Outline.OutlineColor = Color.red;
    }

    public bool MoveForward()
    {
        var fieldWidth = GameManager.Instance.GameMode.FieldWidth();

        Vector3Int position;
        switch (Direction)
        {
            case Direction.Forward:
                if (Position.z == fieldWidth - 1) return false;
                position = Position + Vector3Int.forward;
                break;
            case Direction.Back:
                if (Position.z == 0) return false;
                position = Position + Vector3Int.back;
                break;
            case Direction.Left:
                if (Position.x == 0) return false;
                position = Position + Vector3Int.left;
                break;
            case Direction.Right:
                if (Position.x == fieldWidth - 1) return false;
                position = Position + Vector3Int.right;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Direction), Direction, null);
        }

        var tileBelow = FieldGenerator.Tiles[position.x, position.z];
        position.y = tileBelow.Position.y + 1;
        Position = position;
        return true;
    }

    public void MoveForwardToFloor()
    {
        while (MoveForward())
        {
        }
    }

    public void TurnRight()
    {
        Direction = Direction.TurnRight();
    }

    public void TurnLeft()
    {
        Direction = Direction.TurnLeft();
    }
}

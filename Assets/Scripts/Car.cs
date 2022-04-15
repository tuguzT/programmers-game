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

    public override Vector3Int Position
    {
        get => base.Position;
        protected set
        {
            position = value;
            targetPosition = GetWorldPosition(position);
        }
    }

    private Vector3 targetPosition;

    public override Direction Direction
    {
        get => base.Direction;
        protected set
        {
            direction = value;
            targetRotation = direction.ToQuaternion();
        }
    }

    private Quaternion targetRotation;

    public override void FromData(ITile tile)
    {
        base.Position = tile.Position;
        Position = position;
        base.Direction = tile.Direction;
        Direction = direction;
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

    private void Update()
    {
        if (!IsMoving) return;

        var cachedTransform = transform;
        var time = 10 * Time.deltaTime;
        cachedTransform.position = Vector3.Lerp(cachedTransform.position, targetPosition, time);
        cachedTransform.rotation = Quaternion.Lerp(cachedTransform.rotation, targetRotation, time);
    }

    public bool IsMoving
    {
        get
        {
            const double threshold = 0.001;
            var cachedTransform = transform;
            return Vector3.Distance(cachedTransform.position, targetPosition) > threshold
                   || Mathf.Abs(Quaternion.Angle(cachedTransform.rotation, targetRotation)) > threshold;
        }
    }

    public bool MoveForward()
    {
        var fieldWidth = GameManager.Instance.GameMode.FieldWidth();

        Vector3Int newPosition;
        switch (Direction)
        {
            case Direction.Forward:
                if (Position.z == fieldWidth - 1) return false;
                newPosition = Position + Vector3Int.forward;
                break;
            case Direction.Back:
                if (Position.z == 0) return false;
                newPosition = Position + Vector3Int.back;
                break;
            case Direction.Left:
                if (Position.x == 0) return false;
                newPosition = Position + Vector3Int.left;
                break;
            case Direction.Right:
                if (Position.x == fieldWidth - 1) return false;
                newPosition = Position + Vector3Int.right;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Direction), Direction, null);
        }

        var tileBelow = FieldGenerator.Tiles[newPosition.x, newPosition.z];
        newPosition.y = tileBelow.Position.y + 1;
        Position = newPosition;
        return true;
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

using System;
using System.Collections;
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

    private AudioSource audioSource;
    private Animator animator;
    private static readonly int JumpUpTrigger = Animator.StringToHash("JumpUpTrigger");

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

        audioSource = GetComponentInChildren<AudioSource>();
        animator = GetComponentInChildren<Animator>();

        Outline.OutlineColor = Color.red;
    }

    private void Update()
    {
        if (!IsMoving) return;

        var cachedTransform = transform;
        var time = 8 * Time.deltaTime;
        cachedTransform.position = Vector3.Lerp(cachedTransform.position, targetPosition, time);
        cachedTransform.rotation = Quaternion.Lerp(cachedTransform.rotation, targetRotation, time);
    }

    public bool IsMoving
    {
        get
        {
            const double threshold = 0.001;
            var cachedTransform = transform;
            return audioSource.isPlaying
                   || animator.IsInTransition(0)
                   || Vector3.Distance(cachedTransform.position, targetPosition) > threshold
                   || Mathf.Abs(Quaternion.Angle(cachedTransform.rotation, targetRotation)) > threshold;
        }
    }

    public bool MoveForward()
    {
        var fieldWidth = GameManager.Instance.Difficulty.FieldWidth();

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

        var nextTile = FieldGenerator.Tiles[newPosition.x, newPosition.z];
        if (nextTile.Position.y != Position.y - 1) return false;

        newPosition.y = nextTile.Position.y + 1;
        Position = newPosition;
        audioSource.Play();
        return true;
    }

    public void TurnRight()
    {
        Direction = Direction.TurnRight();
        audioSource.Play();
    }

    public void TurnLeft()
    {
        Direction = Direction.TurnLeft();
        audioSource.Play();
    }

    public void TurnAround()
    {
        Direction = Direction.TurnAround();
        audioSource.Play();
    }

    public bool Jump()
    {
        var fieldWidth = GameManager.Instance.Difficulty.FieldWidth();

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

        var nextTile = FieldGenerator.Tiles[newPosition.x, newPosition.z];
        var heightDiff = nextTile.Position.y - Position.y;
        if (heightDiff is not (0 or -1 or -2)) return false;

        newPosition.y = nextTile.Position.y + 1;
        if (heightDiff == 0)
        {
            animator.SetTrigger(JumpUpTrigger);

            void OnComplete()
            {
                base.Position = newPosition;
                Position = position;
            }

            StartCoroutine(OnAnimationComplete("Jump Up", OnComplete));
        }
        else
            Position = newPosition;
        audioSource.Play();
        return true;
    }

    private IEnumerator OnAnimationComplete(string animationName, Action onComplete)
    {
        yield return new WaitForSeconds(1);
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            yield return null;
        onComplete?.Invoke();
    }
}

using Attributes;
using Model;
using Model.Tile;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public abstract class AbstractTile : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    protected Vector3Int position;

    public virtual Vector3Int Position
    {
        get => position;
        protected set
        {
            position = value;
            transform.position = GetWorldPosition(position);
        }
    }

    [SerializeField]
    [ReadOnly]
    protected Direction direction;

    public virtual Direction Direction
    {
        get => direction;
        protected set
        {
            direction = value;
            transform.rotation = Direction.ToQuaternion();
        }
    }

    public virtual void FromData(ITile tile)
    {
        Position = tile.Position;
        Direction = tile.Direction;
    }

    public FieldGenerator FieldGenerator { get; set; }

    protected Outline Outline { get; private set; }

    protected virtual void Awake()
    {
        Outline = GetComponent<Outline>();

        Outline.enabled = false;
        Outline.OutlineColor = Color.green;
        Outline.OutlineMode = Outline.Mode.OutlineAll;
        Outline.OutlineWidth = 5f;
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0) && !GameManager.Instance.IsGamePaused) Outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Outline.enabled = false;
    }

    protected static Vector3 GetWorldPosition(Vector3Int position)
    {
        var fieldWidth = GameManager.Instance.Difficulty.FieldWidth();
        var offset = new Vector3
        {
            x = (1f - fieldWidth) * ITile.Width / 2f,
            z = (1f - fieldWidth) * ITile.Width / 2f
        };
        var newPosition = new Vector3
        {
            x = position.x * ITile.Width,
            y = position.y * ITile.Height,
            z = position.z * ITile.Width
        };
        return offset + newPosition;
    }
}

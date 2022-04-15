using Attributes;
using Model;
using Model.Tile;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public abstract class BaseTile : MonoBehaviour
{
    [field: SerializeField]
    [field: ReadOnly]
    public Vector3Int Position { get; private set; }

    [field: SerializeField]
    [field: ReadOnly]
    public Direction Direction { get; private set; }

    private ITile data;

    protected ITile Data
    {
        get => data;
        set
        {
            data = value;
            Position = data.Position;
            Direction = data.Direction;

            var fieldWidth = (int)GameManager.Instance.GameMode.FieldWidth();
            var offset = new Vector3
            {
                x = (1f - fieldWidth) * ITile.Width / 2f,
                z = (1f - fieldWidth) * ITile.Width / 2f
            };

            transform.position = offset + new Vector3
            {
                x = Position.x * ITile.Width,
                y = Position.y * ITile.Height,
                z = Position.z * ITile.Width
            };
            transform.rotation = Direction.ToQuaternion();
        }
    }

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
        if (!Input.GetMouseButton(0)) Outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Outline.enabled = false;
    }
}

using Attributes;
using Field;
using Model;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public class Chunk : MonoBehaviour
{
    [field:SerializeField]
    [field:ReadOnly]
    public Vector3Int Position { get; private set; }

    [field:SerializeField]
    [field:ReadOnly]
    public Color Color { get; private set; }

    [field:SerializeField]
    [field:ReadOnly]
    public Direction Direction { get; private set; }

    private ChunkData _data;

    public ChunkData Data
    {
        set
        {
            Position = value.Position;
            Color = value.Color.UnityColor;
            Direction = value.Direction;
            _data = value;

            var fieldWidth = (int) GameManager.Instance.GameMode.FieldWidth();
            var offset = new Vector3
            {
                x = (1f - fieldWidth) * ChunkData.Width / 2f,
                z = (1f - fieldWidth) * ChunkData.Width / 2f,
            };

            transform.position = offset + new Vector3
            {
                x = Position.x * ChunkData.Width,
                y = Position.y * ChunkData.Height,
                z = Position.z * ChunkData.Width,
            };
            transform.rotation = Direction.ToQuaternion();
            _renderer.material.color = Color;
        }
    }

    private Renderer _renderer;
    private Outline _outline;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _outline = GetComponent<Outline>();

        _outline.enabled = false;
        _outline.OutlineColor = Color.green;
        _outline.OutlineMode = Outline.Mode.OutlineAll;
        _outline.OutlineWidth = 5f;
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0)) _outline.enabled = true;
    }

    private void OnMouseExit() => _outline.enabled = false;
}

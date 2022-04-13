using Attributes;
using Field;
using Model;
using UnityEngine;

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

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
}

using Attributes;
using UnityEngine;
using Model.Tile;

public class Tile : BaseTile
{
    [field: SerializeField]
    [field: ReadOnly]
    public Color Color { get; private set; }

    public new TileData Data
    {
        get => (TileData)base.Data;
        set
        {
            base.Data = value;
            Color = Data.Color.UnityColor;
            renderer.material.color = Color;
        }
    }

    private new Renderer renderer;

    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
    }
}

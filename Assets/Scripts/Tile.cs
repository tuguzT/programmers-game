using Attributes;
using Model.Tile;
using UnityEngine;

public class Tile : AbstractTile
{
    [SerializeField]
    [ReadOnly]
    private Color color;

    public Color Color
    {
        get => color;
        private set
        {
            color = value;
            renderer.material.color = Color;
        }
    }

    public override void FromData(ITile tile)
    {
        base.FromData(tile);
        if (tile is TileData tileData)
        {
            Color = tileData.Color.UnityColor;
        }
    }

    private new Renderer renderer;

    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
    }
}

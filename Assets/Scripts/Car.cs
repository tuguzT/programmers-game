using Attributes;
using Model;
using Model.Tile;
using UnityEngine;

public class Car : BaseTile
{
    [field: SerializeField]
    [field: ReadOnly]
    public TeamColor TeamColor { get; private set; }

    public new CarData Data
    {
        get => (CarData)base.Data;
        set
        {
            base.Data = value;
            TeamColor = Data.TeamColor;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Outline.OutlineColor = Color.red;
    }
}

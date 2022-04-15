using Model.Tile;

namespace Field
{
    public interface IFieldGenerator
    {
        uint Width { get; }

        TileData[,] Generate();
    }
}

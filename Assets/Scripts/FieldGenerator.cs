using System.Linq;
using Field;
using Model.Tile;
using UnityEngine;

[RequireComponent(typeof(FieldDistribution))]
public class FieldGenerator : MonoBehaviour
{
    public Tile[,] Tiles { get; private set; }
    public CarData[] Cars { get; private set; }

    public FieldDistribution FieldDistribution { get; private set; }

    private void Awake()
    {
        FieldDistribution = GetComponent<FieldDistribution>();

        var gameMode = GameManager.Instance.Difficulty;
        var fieldGenerator = gameMode.GetFieldGenerator();
        Tiles = new Tile[fieldGenerator.Width, fieldGenerator.Width];

        var generatedTiles = fieldGenerator.Generate();
        for (var i = 0; i < generatedTiles.GetLength(0); i++)
        for (var j = 0; j < generatedTiles.GetLength(1); j++)
        {
            var tileData = generatedTiles[i, j];
            var original = tileData switch
            {
                BaseData @base => FieldDistribution.GetBase(@base.TeamColor),
                LiftData => FieldDistribution.Lift,
                _ => FieldDistribution.GetRandomTile()
            };
            var instantiated = Instantiate(original, Vector3.zero, Quaternion.identity, transform);

            var tile = instantiated.AddComponent<Tile>();
            tile.FieldGenerator = this;
            tile.FromData(tileData);
            Tiles[i, j] = tile;
        }

        var bases = generatedTiles.OfType<BaseData>();
        Cars = CarGenerator.Generate(bases).ToArray();
    }
}

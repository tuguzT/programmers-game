using Field;
using Model.Tile;
using UnityEngine;

[RequireComponent(typeof(FieldDistribution))]
public class FieldGenerator : MonoBehaviour
{
    private Tile[,] tiles;
    private FieldDistribution fieldDistribution;

    private void Awake()
    {
        fieldDistribution = GetComponent<FieldDistribution>();

        var gameMode = GameManager.Instance.GameMode;
        var fieldGenerator = gameMode.GetFieldGenerator();
        tiles = new Tile[fieldGenerator.Width, fieldGenerator.Width];

        var generated = fieldGenerator.Generate();
        for (var i = 0; i < generated.GetLength(0); i++)
        for (var j = 0; j < generated.GetLength(1); j++)
        {
            var tileData = generated[i, j];
            var original = tileData switch
            {
                BaseData @base => fieldDistribution.GetBase(@base.TeamColor),
                LiftData => fieldDistribution.Lift,
                _ => fieldDistribution.GetRandomTile()
            };
            var instantiated = Instantiate(original, Vector3.zero, Quaternion.identity, transform);

            var tile = instantiated.AddComponent<Tile>();
            tile.Data = tileData;
            tiles[i, j] = tile;
        }
    }
}

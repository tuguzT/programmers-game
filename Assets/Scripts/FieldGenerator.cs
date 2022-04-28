using System.Linq;
using Field;
using Model.Tile;
using UnityEngine;

[RequireComponent(typeof(FieldDistribution))]
public class FieldGenerator : MonoBehaviour
{
    public Tile[,] Tiles { get; private set; }
    public Car[] Cars { get; private set; }

    private FieldDistribution fieldDistribution;

    private void Awake()
    {
        fieldDistribution = GetComponent<FieldDistribution>();

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
                BaseData @base => fieldDistribution.GetBase(@base.TeamColor),
                LiftData => fieldDistribution.Lift,
                _ => fieldDistribution.GetRandomTile()
            };
            var instantiated = Instantiate(original, Vector3.zero, Quaternion.identity, transform);

            var tile = instantiated.AddComponent<Tile>();
            tile.FieldGenerator = this;
            tile.FromData(tileData);
            Tiles[i, j] = tile;
        }

        Car CarDataToComponent(CarData carData)
        {
            var original = fieldDistribution.GetCar(carData.TeamColor);
            var instantiated = Instantiate(original, Vector3.zero, Quaternion.identity, transform);

            var car = instantiated.AddComponent<Car>();
            car.FieldGenerator = this;
            car.FromData(carData);
            return car;
        }

        var bases = generatedTiles.OfType<BaseData>();
        Cars = CarGenerator.Generate(bases).Select(CarDataToComponent).ToArray();
    }
}

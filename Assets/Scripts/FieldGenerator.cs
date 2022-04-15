using System.Linq;
using Field;
using Model.Tile;
using UnityEngine;

[RequireComponent(typeof(FieldDistribution))]
public class FieldGenerator : MonoBehaviour
{
    private Tile[,] tiles;
    private Car[] cars;
    private FieldDistribution fieldDistribution;

    private void Awake()
    {
        fieldDistribution = GetComponent<FieldDistribution>();

        var gameMode = GameManager.Instance.GameMode;
        var fieldGenerator = gameMode.GetFieldGenerator();
        tiles = new Tile[fieldGenerator.Width, fieldGenerator.Width];

        var (generatedTiles, generatedCars) = fieldGenerator.Generate();
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
            tile.Data = tileData;
            tiles[i, j] = tile;
        }

        Car CarDataToComponent(CarData carData, int _)
        {
            var original = fieldDistribution.GetCar(carData.TeamColor);
            var instantiated = Instantiate(original, Vector3.zero, Quaternion.identity, transform);
            var car = instantiated.AddComponent<Car>();
            car.Data = carData;
            return car;
        }

        cars = generatedCars.Select(CarDataToComponent).ToArray();
    }
}

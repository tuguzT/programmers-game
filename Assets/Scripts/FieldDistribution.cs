using System.Linq;
using UnityEngine;

public class FieldDistribution : MonoBehaviour
{
    [SerializeField]
    private GameObject[] commonTiles;

    [SerializeField]
    [Range(0, 100)]
    private int commonChance = 100;

    [SerializeField]
    private GameObject[] rareTiles;

    [SerializeField]
    [Range(0, 100)]
    private int rareChance = 2;

    public GameObject GetRandomTile()
    {
        var common = commonTiles.Select((obj, _) => (obj, commonChance));
        var rare = rareTiles.Select((obj, _) => (obj, rareChance));
        var shuffled = common.Concat(rare).OrderBy(_ => Random.Range(0f, 1f)).ToList();

        var tiles = shuffled.Select((tuple, _) => tuple.obj);
        var chances = shuffled.Select((tuple, _) => tuple.Item2).ToList();

        var sum = chances.Sum();
        var randomValue = Random.Range(0, sum + 1);
        sum = 0;
        foreach (var (chance, i) in chances.Select((chance, i) => (chance, i)))
        {
            sum += chance;
            if (randomValue <= sum) return tiles.ElementAt(i);
        }

        return tiles.Last();
    }
}

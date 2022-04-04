using System.Linq;
using UnityEngine;

public class FieldDistribution : MonoBehaviour
{
    [SerializeField]
    private GameObject[] commonTerrains;

    [SerializeField]
    [Range(0, 100)]
    private int commonChance = 100;

    [SerializeField]
    private GameObject[] rareTerrains;

    [SerializeField]
    [Range(0, 100)]
    private int rareChance = 2;

    public GameObject GetRandomTerrain()
    {
        var common = commonTerrains.Select((obj, _) => (obj, commonChance));
        var rare = rareTerrains.Select((obj, _) => (obj, rareChance));
        var shuffled = common.Concat(rare).OrderBy(_ => Random.Range(int.MinValue, int.MaxValue)).ToList();

        var terrains = shuffled.Select((tuple, _) => tuple.obj);
        var chances = shuffled.Select((tuple, _) => tuple.Item2).ToList();

        var sum = chances.Sum();
        var randomValue = Random.Range(0, sum + 1);
        sum = 0;
        foreach (var (chance, i) in chances.Select((chance, i) => (chance, i)))
        {
            sum += chance;
            if (randomValue <= sum) return terrains.ElementAt(i);
        }

        return terrains.Last();
    }
}

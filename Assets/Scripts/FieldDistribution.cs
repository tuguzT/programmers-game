using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public class FieldDistribution : MonoBehaviour
{
    [Header("Tiles")] [SerializeField] private GameObject[] commonTiles;

    [SerializeField] [Range(0, 100)] private int commonChance = 100;

    [SerializeField] private GameObject[] rareTiles;

    [SerializeField] [Range(0, 100)] private int rareChance = 2;

    [field: Header("Lifts")]
    [field: SerializeField]
    public GameObject Lift { get; private set; }

    [Header("Difficulties")] [SerializeReference]
    private DifficultyDistribution easy = new();

    [SerializeReference] private DifficultyDistribution hard = new();

    private class DifficultyDistribution
    {
        [SerializeReference] public TeamDistribution Blue = new();

        [SerializeReference] public TeamDistribution Green = new();

        [SerializeReference] public TeamDistribution Red = new();

        [SerializeReference] public TeamDistribution Yellow = new();
    }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    [SuppressMessage("ReSharper", "Unity.RedundantSerializeFieldAttribute")]
    private class TeamDistribution
    {
        [SerializeField] public GameObject Base;

        [SerializeField] public GameObject Car;
    }

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

    private DifficultyDistribution GetDifficultyDistribution(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => easy,
            Difficulty.Hard => hard,
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
        };
    }

    private TeamDistribution GetTeamDistribution(TeamColor teamColor)
    {
        var difficultyDistribution = GetDifficultyDistribution(GameManager.Instance.Difficulty);
        return teamColor switch
        {
            TeamColor.Red => difficultyDistribution.Red,
            TeamColor.Green => difficultyDistribution.Green,
            TeamColor.Yellow => difficultyDistribution.Yellow,
            TeamColor.Blue => difficultyDistribution.Blue,
            _ => throw new ArgumentOutOfRangeException(nameof(teamColor), teamColor, null)
        };
    }

    public GameObject GetBase(TeamColor teamColor)
    {
        return GetTeamDistribution(teamColor).Base;
    }

    public GameObject GetCar(TeamColor teamColor)
    {
        return GetTeamDistribution(teamColor).Car;
    }
}

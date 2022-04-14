using Field;
using UnityEngine;

[RequireComponent(typeof(FieldDistribution))]
public class FieldGenerator : MonoBehaviour
{
    private Chunk[,] _chunks;
    private FieldDistribution _fieldDistribution;

    private void Awake()
    {
        _fieldDistribution = GetComponent<FieldDistribution>();

        var gameMode = GameManager.Instance.GameMode;
        var field = gameMode.Field();
        _chunks = new Chunk[field.Width, field.Width];

        var chunks = field.Generate();
        for (var i = 0; i < chunks.GetLength(0); i++)
        for (var j = 0; j < chunks.GetLength(1); j++)
        {
            var chunk = chunks[i, j];
            var tile = chunk switch
            {
                Base @base => _fieldDistribution.GetBase(@base.CarColor),
                _ => _fieldDistribution.GetRandomTile()
            };
            var instantiated = Instantiate(tile, Vector3.zero, Quaternion.identity, transform);

            var chunkComponent = instantiated.AddComponent<Chunk>();
            chunkComponent.Data = chunk;
            _chunks[i, j] = chunkComponent;
        }
    }
}

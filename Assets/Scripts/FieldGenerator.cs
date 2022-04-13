using Field;
using Model;
using UnityEngine;

[RequireComponent(typeof(FieldDistribution))]
public class FieldGenerator : MonoBehaviour
{
    private Chunk[,] _chunks;
    private FieldDistribution _fieldDistribution;

    private void Awake()
    {
        _fieldDistribution = GetComponent<FieldDistribution>();

        var fieldWidth = (int) GameManager.Instance.GameMode.FieldWidth();
        _chunks = new Chunk[fieldWidth, fieldWidth];

        var field = GameManager.Instance.GameMode.Field();
        var generatedData = field.Generate();
        for (var i = 0; i < generatedData.GetLength(0); i++)
        {
            for (var j = 0; j < generatedData.GetLength(1); j++)
            {
                var terrain = _fieldDistribution.GetRandomTerrain();
                var instantiated = Instantiate(terrain, Vector3.zero, Quaternion.identity, transform);

                var chunk = instantiated.AddComponent<Chunk>();
                chunk.Data = generatedData[i, j];
                _chunks[i, j] = chunk;
            }
        }
    }
}

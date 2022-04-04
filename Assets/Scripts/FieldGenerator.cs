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
        var offset = new Vector3
        {
            x = (1f - fieldWidth) * ChunkData.Width / 2f,
            z = (1f - fieldWidth) * ChunkData.Width / 2f,
        };

        _chunks = new Chunk[fieldWidth, fieldWidth];
        var field = GameManager.Instance.GameMode.Field();
        var generatedData = field.Generate();
        for (var i = 0; i < generatedData.GetLength(0); i++)
        {
            for (var j = 0; j < generatedData.GetLength(1); j++)
            {
                var data = generatedData[i, j];

                var terrain = _fieldDistribution.GetRandomTerrain();
                var position = offset + new Vector3
                {
                    x = data.position.x * ChunkData.Width,
                    y = data.position.y * ChunkData.Height,
                    z = data.position.z * ChunkData.Width,
                };
                var rotation = DirectionHelper.GetRandom().ToQuaternion();

                var instantiated = Instantiate(terrain, position, rotation, transform);
                instantiated.GetComponent<Renderer>().material.color = data.color;
                var chunk = instantiated.AddComponent<Chunk>();
                chunk.Data = data;
                _chunks[i, j] = chunk;
            }
        }
    }
}

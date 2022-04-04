using System.Collections.Generic;
using Field;
using Model;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    // ReSharper disable once CollectionNeverQueried.Local
    private readonly List<Chunk> _chunks = new List<Chunk>();

    private void Awake()
    {
        var fieldSize = (int) GameManager.Instance.GameMode.FieldSize();

        var prefabSize = prefab.GetComponentInChildren<Renderer>().bounds.size;
        print(prefabSize);
        var offset = new Vector3
        {
            x = (1f - fieldSize) * prefabSize.x / 2f,
            y = 0f,
            z = (1f - fieldSize) * prefabSize.z / 2f,
        };

        var field = GameManager.Instance.GameMode.Field();
        var chunks = field.Generate();
        foreach (var data in chunks)
        {
            var position = offset + new Vector3
            {
                x = data.position.x * prefabSize.x,
                y = data.position.y * prefabSize.y,
                z = data.position.z * prefabSize.z,
            };
            var instantiated = Instantiate(prefab, position, Quaternion.identity, transform);
            instantiated.GetComponent<Renderer>().material.color = data.color;
            var chunk = instantiated.AddComponent<Chunk>();
            chunk.Data = data;
            _chunks.Add(chunk);
        }
    }
}

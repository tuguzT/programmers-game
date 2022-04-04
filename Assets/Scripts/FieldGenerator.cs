using System.Collections.Generic;
using Field;
using Model;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    // ReSharper disable once CollectionNeverQueried.Local
    private readonly List<GameObject> _prefabs = new List<GameObject>();

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
        foreach (var chunk in chunks)
        {
            var position = offset + new Vector3
            {
                x = chunk.Position.x * prefabSize.x,
                y = chunk.Position.y * prefabSize.y,
                z = chunk.Position.z * prefabSize.z,
            };
            var instantiated = Instantiate(prefab, position, Quaternion.identity, transform);
            instantiated.GetComponentInChildren<Renderer>().material.color = chunk.Color;
            _prefabs.Add(instantiated);
        }
    }
}

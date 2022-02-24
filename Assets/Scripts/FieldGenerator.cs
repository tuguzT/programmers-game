using System.Collections.Generic;
using Field;
using Model;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private readonly List<GameObject> _prefabs = new List<GameObject>();
    private Vector3 _offset;

    private void Start()
    {
        var gameMode = GameManager.Instance.GameMode;
        var size = (int) gameMode.FieldSize();
        _offset = new Vector3((1 - size) / 2f, 0f, (1 - size) / 2f);

        var field = gameMode.Field();
        var chunks = field.Generate();
        foreach (var chunk in chunks)
        {
            var position = chunk.Position + _offset;
            var instantiated = Instantiate(prefab, position, Quaternion.identity, transform);
            instantiated.GetComponent<Renderer>().material.color = chunk.Color;
            _prefabs.Add(instantiated);
        }
        print(_prefabs);
    }
}

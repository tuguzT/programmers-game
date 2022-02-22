using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    [Range(6, 9)]
    private int size = 6;

    private readonly List<GameObject> _prefabs = new List<GameObject>();
    private Vector3 _offset;

    private void Start()
    {
        _offset = new Vector3((1 - size) / 2f, 0f, (1 - size) / 2f);
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var position = new Vector3(i, 0, j) + _offset;
                var instantiated = Instantiate(prefab, position, Quaternion.identity, transform);
                _prefabs.Add(instantiated);
            }
        }
        print(_prefabs);
    }
}

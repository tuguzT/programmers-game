using System;
using Model;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [field: SerializeField]
    public GameMode GameMode { get; private set; }

    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) throw new Exception("It is an error to instantiate new GameManager instance");
        Instance = this;
    }

    #endregion
}

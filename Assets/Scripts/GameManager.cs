using System;
using Model;
using UnityEngine;

[RequireComponent(typeof(MusicManager))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [field: SerializeField] public GameMode GameMode { get; set; }

    [field: SerializeField] public bool IsGamePaused { get; set; }

    [SerializeField] [Range(2, 4)] private byte playerCount = 4;

    public byte PlayerCount
    {
        get => playerCount;
        set
        {
            if (value is < 2 or > 4)
                throw new ArgumentException($"Player count must be between 2 and 4, got {value}", nameof(value));
            playerCount = value;
        }
    }

    public string LoadSceneName { get; set; }
}

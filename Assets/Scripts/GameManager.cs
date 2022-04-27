using Model;
using UnityEngine;

[RequireComponent(typeof(MusicManager))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [field: SerializeField] public GameMode GameMode { get; private set; }
}

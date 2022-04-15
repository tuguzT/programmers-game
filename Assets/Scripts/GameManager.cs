using Model;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [field: SerializeField] public GameMode GameMode { get; private set; }

    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}

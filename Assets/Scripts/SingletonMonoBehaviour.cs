using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = (T) this;
        DontDestroyOnLoad(gameObject);
    }
}

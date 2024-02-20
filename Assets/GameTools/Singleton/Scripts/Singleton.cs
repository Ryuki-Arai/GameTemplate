using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static readonly T _instance = new T();

    public static T I => _instance;

    protected Singleton()
    {
#if DEBUG_UNITY
        Debug.Assert(_instance == null);
#endif
    }
}

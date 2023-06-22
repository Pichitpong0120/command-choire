using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set;}

    public void Awake()
    {
        instance = (T) (object) this;
    }
}

using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Vector3 startSpawn;

    void Start()
    {
        transform.position = startSpawn;
    }
}
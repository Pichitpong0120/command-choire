using UnityEngine;

public class DogComponent : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
    }
}

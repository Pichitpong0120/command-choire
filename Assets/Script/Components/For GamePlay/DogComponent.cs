using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogComponent : MonoBehaviour
{
    [SerializeField] int index = 0;
    [SerializeField] List<GameObject> dirMovement;
    Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    public void Movement()
    {
        if (index < dirMovement.Count) index++;
        else index = 0;
        transform.position = dirMovement[index].transform.position;

        // Vector3 positionDog = new(transform.position.x, transform.position.y, modifierRayCheck);
        // positionDog += newMovement;
        // if (Physics2D.OverlapCircle(positionDog, 0.2f, layerCanWalk) == null) return;
        // transform.position += newMovement;
    }

    public void ResetGame()
    {
        transform.position = startPosition;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
    }
}

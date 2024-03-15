using System.Collections.Generic;
using UnityEngine;

public class DogComponent : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] List<GameObject> dirMovement;
    [SerializeField] float modifierRayCheck = 24;
    [SerializeField] LayerMask layerCanWalk;
    Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    private void Movement(Vector3 newMovement)
    {
        Vector3 positionDog = new(transform.position.x, transform.position.y, modifierRayCheck);
        positionDog += newMovement;
        if (Physics2D.OverlapCircle(positionDog, 0.2f, layerCanWalk) == null) return;
        transform.position += newMovement;
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

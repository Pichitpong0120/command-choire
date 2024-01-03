using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Vector3 startSpawn;

    void Start()
    {
        startSpawn = transform.position;
    }

    public void ResetGame()
    {
        transform.position = startSpawn;
    }

    public void PlayerMoveUp()
    {
        Vector2 transformPlayer = transform.position;
        transformPlayer.y += 1;
        transform.position = transformPlayer;
    }

    public void PlayerMoveDown()
    {
        Vector2 transformPlayer = transform.position;
        transformPlayer.y -= 1;
        transform.position = transformPlayer;
    }

    public void PlayerMoveLeft()
    {
        Vector2 transformPlayer = transform.position;
        transformPlayer.x -= 1;
        transform.position = transformPlayer;
    }

    public void PlayerMoveRight()
    {
        Vector2 transformPlayer = transform.position;
        transformPlayer.x += 1;
        transform.position = transformPlayer;
    }
}
using UnityEngine;

namespace CommandChoice.Component
{
    public class CameraManager : MonoBehaviour
    {
        //[SerializeField] private Camera camera;
        [SerializeField] private GameObject player;
        [SerializeField] private Vector2 offset;

        void Awake()
        {
            //camera = GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z - 10f);
        }
    }
}
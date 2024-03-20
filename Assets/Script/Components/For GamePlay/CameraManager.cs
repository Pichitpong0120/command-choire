using CommandChoice.Model;
using UnityEngine;

namespace CommandChoice.Component
{
    public class CameraManager : MonoBehaviour
    {
        //[SerializeField] private Camera camera;
        [SerializeField] private GameObject player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothTime = 0.25f;
        [SerializeField] private Vector3 velocity = Vector3.zero;

        void Awake()
        {
            //camera = GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag(StaticText.TagPlayer);
        }

        void Update()
        {
            Vector3 targetPosition = player.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
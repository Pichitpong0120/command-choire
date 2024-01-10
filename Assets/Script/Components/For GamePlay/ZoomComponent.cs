using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class ZoomComponent : MonoBehaviour
    {
        [Header("Fields Auto Set")]
        [SerializeField] Button button;
        [SerializeField] Image image;
        [SerializeField] Camera Camera;
        [SerializeField] bool zoomActive;
        [SerializeField] float zoomSmooth = 8f;
        [SerializeField] float minZoom = 3f;
        [SerializeField] float maxZoom = 6f;

        void Awake()
        {
            zoomActive = false;
            Camera = GameObject.FindGameObjectWithTag(StaticText.TagCamera).GetComponent<Camera>();
            button = GetComponent<Button>();
            image = transform.GetChild(0).GetComponent<Image>();
        }

        void Start()
        {
            button.onClick.AddListener(() =>
            {
                zoomActive = !zoomActive;

                CheckScreenSize();
                if (zoomActive)
                {
                    image.sprite = Resources.Load<Sprite>(StaticText.PathImgMinimize);
                }
                else
                {
                    image.sprite = Resources.Load<Sprite>(StaticText.PathImgZoom);
                }
            });
        }

        void Update()
        {
            if (zoomActive && Camera.orthographicSize <= maxZoom)
            {
                Camera.orthographicSize += Time.deltaTime * zoomSmooth;
            }
            else if (!zoomActive && Camera.orthographicSize > minZoom)
            {
                Camera.orthographicSize -= Time.deltaTime * zoomSmooth;
            }
        }

        void CheckScreenSize()
        {
            if (Screen.height >= 1500f) { maxZoom = 9.25f; minZoom = 4.25f; }
            if (Screen.height <= 900f) { maxZoom = 6.9f; minZoom = 3.5f; }
            else { maxZoom = 6f; minZoom = 3f; }
            // print(Screen.height + " " + Screen.width);
            // print("Platform" + Application.platform);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class AddCommand : MonoBehaviour
{
    [SerializeField] private GameObject uiPopup;
    private Transform parentToSpawn;
    private Button button;

    void Awake()
    {
        parentToSpawn = GameObject.FindGameObjectWithTag("Canvas").transform;
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(() => {SceneGameManager.SpawnGameObject(uiPopup, parentToSpawn);});
    }

    void Update()
    {
        int currentSiblingIndex = transform.GetSiblingIndex();
        int lastIndex = transform.parent.childCount - 1;

        if (currentSiblingIndex != lastIndex)
        {
            transform.SetSiblingIndex(lastIndex);
        }
    }
}
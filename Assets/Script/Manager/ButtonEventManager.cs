using UnityEngine;

public class ButtonEventManager : MonoBehaviour
{
    [field: SerializeField] public ButtonManager button { get; private set; }

    private void Start()
    {
        ButtonListenManager.AddListener(button);
    }

    public static void LoadScene(string sceneName)
    {
        SceneGameManager.LoadScene(sceneName);
    }
}

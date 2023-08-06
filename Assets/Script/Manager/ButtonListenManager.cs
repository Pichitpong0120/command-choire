using UnityEngine.UI;

public class ButtonListenManager
{
    public static void AddListener(ButtonManager buttons)
    {
        if (buttons.loadScene.Count != 0)
        {
            foreach (ButtonLoadScene button in buttons.loadScene)
            {
                if (button.active) button.buttonObject.onClick.AddListener(() => ButtonEventManager.LoadScene(button.nameScene));
            }
        }

        if (buttons.selectLevel.Count != 0)
        {
            foreach (ButtonSelectLevel button in buttons.selectLevel)
            {
                if (button.unLock)button.buttonObject.onClick.AddListener(() => ButtonEventManager.LoadScene(button.nameLevel));
            }
        }
    }

    public static void AddListenerLoadScene(Button button, string sceneName)
    {
        button.onClick.AddListener(() => ButtonEventManager.LoadScene(sceneName));
    }
}

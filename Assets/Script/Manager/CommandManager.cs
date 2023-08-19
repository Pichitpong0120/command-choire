using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private ButtonManager buttons;
    void Start()
    {
        buttons = this.GetComponent<ButtonEventManager>().button;

        foreach (var level in DataManager.DeployGetData())
        {
            buttons.selectLevel.Add(new ButtonSelectLevel(level.indexLevel, level.nameLevel, level.unLock, level.mail, level.score));
        }
    }
}

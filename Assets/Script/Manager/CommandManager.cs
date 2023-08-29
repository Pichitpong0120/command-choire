using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private ButtonManager buttons;
    void Start()
    {
        buttons = this.GetComponent<ButtonEventManager>().button;
        ButtonSelectLevelDetails detailsLevel;
        foreach (var level in DataManager.DeployGetData())
        {
            detailsLevel = new ButtonSelectLevelDetails(level.details.unLock, level.details.mail, level.details.score);
            buttons.selectLevel.Add(new ButtonSelectLevel(level.indexLevel, level.nameLevel, detailsLevel));
        }
    }
}

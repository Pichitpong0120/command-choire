using UnityEngine;
using System.Collections.Generic;
using System;

public class CommandManager : MonoBehaviour
{
    [SerializeField] private Transform boxCommandParent;
    public List<GameObject> boxCommandList { get; private set; }
    private bool runCommandAction;
    private ButtonManager buttons;

    void Start()
    {
        boxCommandList = new List<GameObject>();
        buttons = this.GetComponent<ButtonEventManager>().button;
        ButtonSelectLevelDetails detailsLevel;
        var data = DataManager.DeployGetData();
        if (data != null)
        {
            foreach (var level in data)
            {
                detailsLevel = new ButtonSelectLevelDetails(level.details.unLock, level.details.mail, level.details.score);
                buttons.selectLevel.Add(new ButtonSelectLevel(level.indexLevel, level.nameLevel, detailsLevel));
            }
        }
        else
        {
            Debug.Log("Data Equal Null");
        }
    }

    void Update()
    {
        RunContentBoxCommand(runCommandAction);
    }

    private void RunContentBoxCommand(bool runCommandAction)
    {
        if (!runCommandAction) return;
        ReadContentBoxCommandList();
    }

    public void ReadContentBoxCommandList()
    {
        var commandList = boxCommandParent.GetComponentsInChildren<Command>();
        foreach (var commandObject in commandList)
        {
            boxCommandList.Add(commandObject.gameObject);
            runCommandAction = true;
        }
    }
}
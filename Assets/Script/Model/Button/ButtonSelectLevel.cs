using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonSelectLevel
{
    [field: SerializeField] public Button buttonObject { get; set; }
    [field: SerializeField] public int indexLevel { get; set; }
    [field: SerializeField] public string nameLevel { get; set; }
    [field: SerializeField] public ButtonSelectLevelDetails details { get; set; }

    public ButtonSelectLevel(Button button, int index, string name, ButtonSelectLevelDetails details)
    {
        buttonObject = button;
        indexLevel = index;
        nameLevel = name;
        this.details = details;
    }
    public ButtonSelectLevel(int index, string name, ButtonSelectLevelDetails details)
    {
        buttonObject = null;
        indexLevel = index;
        nameLevel = name;
        this.details = details;
    }
    public ButtonSelectLevel(Button button, int index, string name)
    {
        buttonObject = button;
        indexLevel = index;
        nameLevel = name;
        details = new ButtonSelectLevelDetails();
    }
}

[Serializable]
public class ButtonSelectLevelDetails
{
    [field: SerializeField] public bool unLock { get; set; }
    [field: SerializeField] public int mail { get; set; }
    [field: SerializeField] public float score { get; set; }
    [field: SerializeField] public string useTime { get; set; }
    [field: SerializeField] public string countBoxCommand { get; set; }

    public ButtonSelectLevelDetails(ButtonSelectLevelDetailsData detailsData)
    {
        if (detailsData != null)
        {
            unLock = detailsData.unLock;
            mail = detailsData.mail;
            score = detailsData.score;
            useTime = detailsData.useTime;
            countBoxCommand = detailsData.countBoxCommand;
        }
        else
        {
            unLock = false;
            mail = 0;
            score = 0;
            useTime = "";
            countBoxCommand = "";
        }
    }

    public ButtonSelectLevelDetails(bool unLock = false, int mail = 0, float score = 0, string useTime = "", string countBoxCommand = "")
    {
        this.unLock = unLock;
        this.mail = mail;
        this.score = score;
        this.useTime = useTime;
        this.countBoxCommand = countBoxCommand;
    }

    public ButtonSelectLevelDetails()
    {
    }
}

[Serializable]
public class ButtonSelectLevelData
{
    public int indexLevel;
    public string nameLevel;
    public ButtonSelectLevelDetailsData details;

    public ButtonSelectLevelData()
    {
    }

    public ButtonSelectLevelData(int index, string name, ButtonSelectLevelDetailsData details)
    {
        indexLevel = index;
        nameLevel = name;
        this.details = details;
    }
}

[Serializable]
public class ButtonSelectLevelDetailsData
{
    public bool unLock;
    public int mail;
    public float score;
    public string useTime;
    public string countBoxCommand;

    public ButtonSelectLevelDetailsData(bool unLock = false, int mail = 0, float score = 0)
    {
        this.unLock = unLock;
        this.mail = mail;
        this.score = score;
    }

    public ButtonSelectLevelDetailsData(ButtonSelectLevelDetails details)
    {
        if (details != null)
        {
            unLock = details.unLock;
            mail = details.mail;
            score = details.score;
            useTime = details.useTime;
            countBoxCommand = details.countBoxCommand;
        }
        else
        {
            unLock = false;
            mail = 0;
            score = 0;
            useTime = "";
            countBoxCommand = "";
        }
    }
}

[Serializable]
public class ButtonSelectLevelDataWrapper
{
    public List<ButtonSelectLevelData> levelData;

    public ButtonSelectLevelDataWrapper(List<ButtonSelectLevelData> levelData)
    {
        this.levelData = levelData;
    }
}
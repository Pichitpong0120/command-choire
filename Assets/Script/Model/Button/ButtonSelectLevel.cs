using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonSelectLevel
{
    [field: SerializeField] public Button buttonObject { get; set; }
    [field: SerializeField] public int indexLevel  { get; set; }
    [field: SerializeField] public string nameLevel  { get; set; }
    [field: SerializeField] public bool unLock  { get; set; }
    [field: SerializeField] public int mail  { get; set; }
    [field: SerializeField] public float score  { get; set; }

    public ButtonSelectLevel(Button button, int index, string name, bool unLock, int mail, float score)
    {
        buttonObject = button;
        indexLevel = index;
        nameLevel = name;
        this.unLock = unLock;
        this.mail = mail;
        this.score = score;
    }
    public ButtonSelectLevel(int index, string name, bool unLock, int mail, float score)
    {
        buttonObject = null;
        indexLevel = index;
        nameLevel = name;
        this.unLock = unLock;
        this.mail = mail;
        this.score = score;
    }
    public ButtonSelectLevel(Button button, int index, string name)
    {
        buttonObject = button;
        indexLevel = index;
        nameLevel = name;
    }
}

[Serializable]
public class ButtonSelectLevelData
{
    public int indexLevel;
    public string nameLevel;
    public bool unLock;
    public int mail;
    public float score;

    public ButtonSelectLevelData()
    {
    }

    public ButtonSelectLevelData(int index, string name, bool unLock, int mail, float score)
    {
        indexLevel = index;
        nameLevel = name;
        this.unLock = unLock;
        this.mail = mail;
        this.score = score;
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
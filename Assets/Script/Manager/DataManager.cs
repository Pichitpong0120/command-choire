using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    private static List<ButtonSelectLevelData> data;

    public static void DeployToData(List<ButtonSelectLevel> levels)
    {
        if (data == null)
        {
            data = new List<ButtonSelectLevelData>(levels.Count);
            ButtonSelectLevelData levelData;
            ButtonSelectLevelDetailsData detailsData;

            foreach (ButtonSelectLevel level in levels)
            {
                detailsData = new ButtonSelectLevelDetailsData(level.details.unLock, level.details.mail, level.details.score);
                levelData = new ButtonSelectLevelData(level.indexLevel, level.nameLevel, detailsData);
                data.Add(levelData);
            }
        }
    }

    public static List<ButtonSelectLevelData> DeployGetData()
    {
        List<ButtonSelectLevelData> newData = data;
        ClearData();
        return newData;
    }

    public static void ClearData()
    {
        data = null;
    }

    public static bool CheckJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");
        return File.Exists(filePath);
    }

    public static void SaveToJson(List<ButtonSelectLevel> buttons)
    {
        List<ButtonSelectLevelData> LevelsData = new();
        ButtonSelectLevelDetailsData detailsData;
        foreach (ButtonSelectLevel button in buttons)
        {
            detailsData = new ButtonSelectLevelDetailsData(button.details);
            LevelsData.Add(new ButtonSelectLevelData(button.indexLevel, button.nameLevel, detailsData));
        }

        string jsonData = JsonUtility.ToJson(new ButtonSelectLevelDataWrapper(LevelsData));
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");
        File.WriteAllText(filePath, jsonData);
    }

    public static List<ButtonSelectLevelData> LoadFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ButtonSelectLevelDataWrapper dataWrapper = JsonUtility.FromJson<ButtonSelectLevelDataWrapper>(jsonData);

            return dataWrapper.levelData;
        }

        return null;
    }

    public static void LoadFromJson(List<ButtonSelectLevel> button)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ButtonSelectLevelDataWrapper dataWrapper = JsonUtility.FromJson<ButtonSelectLevelDataWrapper>(jsonData);
            ButtonSelectLevelDetails details;
            for (int i = 0; i < button.Count; i++)
            {
                details = new(dataWrapper.levelData[i].details);
                button[i].indexLevel = dataWrapper.levelData[i].indexLevel;
                button[i].nameLevel = dataWrapper.levelData[i].nameLevel;
                button[i].details.unLock = dataWrapper.levelData[i].details.unLock;
                button[i].details.mail = dataWrapper.levelData[i].details.mail;
                button[i].details.score = dataWrapper.levelData[i].details.score;
                button[i].details = details;
            }
        }
    }
}
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public static List<ButtonSelectLevelData> data = new();

    public static void DeployToData(List<ButtonSelectLevel> levels)
    {
        if (data.Count == 0)
        {
            foreach (ButtonSelectLevel level in levels)
            {
                data.Add(new ButtonSelectLevelData(level.indexLevel, level.nameLevel, level.unLock, level.mail, level.score));
            }
        }
    }

    public static List<ButtonSelectLevelData> DeployGetData()
    {
        return data;
    }

    public static void ClearData()
    {
        data = new List<ButtonSelectLevelData>();
    }

    public static bool CheckJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");
        return File.Exists(filePath);
    }

    public static void SaveToJson(List<ButtonSelectLevel> buttons)
    {
        List<ButtonSelectLevelData> LevelsData = new();

        foreach (ButtonSelectLevel button in buttons)
        {
            LevelsData.Add(new ButtonSelectLevelData(button.indexLevel, button.nameLevel, button.unLock, button.mail, button.score));
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

            for(int i = 0; i < button.Count; i++)
            {
                button[i].indexLevel = dataWrapper.levelData[i].indexLevel;
                button[i].nameLevel = dataWrapper.levelData[i].nameLevel;
                button[i].unLock = dataWrapper.levelData[i].unLock;
                button[i].mail = dataWrapper.levelData[i].mail;
                button[i].score = dataWrapper.levelData[i].score;
            }
        }
    }
}
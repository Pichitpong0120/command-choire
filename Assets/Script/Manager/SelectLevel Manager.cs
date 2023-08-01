using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class SelectLevelManager : MonoBehaviour
{
    [field: SerializeField] private GameObject btnSelectLevelPrefab;
    [field: SerializeField] private Transform parentBtnSelectLevel;
    [field: SerializeField] private Button btnBackToMenu;

    void Start()
    {
        InitializeBTNSelectLevel();

        btnBackToMenu.onClick.AddListener(() => SceneGameManager.LoadScene("MainMenu"));
    }

    void InitializeBTNSelectLevel()
    {
        List<string> levels = SceneGameManager.SceneLevelNames();

        levels = levels.OrderBy(level => int.Parse(level.Substring(6))).ToList();

        for (int i = 0; i < levels.Count; i++)
        {
            GameObject clonedButton = Instantiate(btnSelectLevelPrefab, parentBtnSelectLevel);

            BtnSelectLevelData btnSelectLevel = clonedButton.GetComponent<BtnSelectLevelData>();
            btnSelectLevel.indexLevel = i+1;
            btnSelectLevel.nameLevel = levels[i];
        }
        LoadLevelFromJson();
    }

    void SaveLevelToJson()
    {
        List<LevelsData> LevelsData = new List<LevelsData>();

        BtnSelectLevelData[] btnSelectLevels = parentBtnSelectLevel.GetComponentsInChildren<BtnSelectLevelData>();

        foreach (BtnSelectLevelData btnSelectLevel in btnSelectLevels)
        {
            LevelsData.Add(new LevelsData(btnSelectLevel.indexLevel, btnSelectLevel.nameLevel, btnSelectLevel.unLock));
        }

        string jsonData = JsonUtility.ToJson(new LevelsDataWrapper(LevelsData));
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");
        File.WriteAllText(filePath, jsonData);
    }

    void LoadLevelFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "level_data.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            LevelsDataWrapper dataWrapper = JsonUtility.FromJson<LevelsDataWrapper>(jsonData);
            BtnSelectLevelData[] btnSelectLevels = parentBtnSelectLevel.GetComponentsInChildren<BtnSelectLevelData>();
            for (int i = 0; i < btnSelectLevels.Length; i++)
            {
                btnSelectLevels[i].indexLevel = dataWrapper.LevelsData[i].indexLevel;
                btnSelectLevels[i].nameLevel = dataWrapper.LevelsData[i].nameLevel;
                btnSelectLevels[i].unLock = dataWrapper.LevelsData[i].unLock;
            }
        }
        else
        {
            BtnSelectLevelData[] btnSelectLevels = parentBtnSelectLevel.GetComponentsInChildren<BtnSelectLevelData>();
            btnSelectLevels[0].unLock = true;
            SaveLevelToJson();
        }
    }
}

[System.Serializable]
public class LevelsData
{
    public int indexLevel;
    public string nameLevel;
    public bool unLock;

    public LevelsData(int indexLevel, string nameLevel, bool unLock)
    {
        this.indexLevel = indexLevel;
        this.nameLevel = nameLevel;
        this.unLock = unLock;
    }
}

[System.Serializable]
public class LevelsDataWrapper
{
    public List<LevelsData> LevelsData;

    public LevelsDataWrapper(List<LevelsData> LevelsData)
    {
        this.LevelsData = LevelsData;
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonSelectLevelPrefab;
    [SerializeField] private Transform parentButtonSelectLevel;

    private ButtonEventManager data;

    private void Start()
    {
        data = GetComponent<ButtonEventManager>();

        InitializedData(GenerateSelectLevel());
    }

    List<GameObject> GenerateSelectLevel()
    {
        int index = 0;
        List<GameObject> buttonsSelectLevel = new();

        foreach (string levelName in SceneGameManager.SceneLevelNames())
        {
            index++;
            GameObject buttonSelectLevel = Instantiate(buttonSelectLevelPrefab, parentButtonSelectLevel);
            buttonSelectLevel.name = levelName;
            buttonsSelectLevel.Add(buttonSelectLevel);
            data.button.selectLevel.Add(new ButtonSelectLevel(buttonSelectLevel.GetComponent<Button>(), index, levelName));
        }

        return buttonsSelectLevel;
    }
    void InitializedData(List<GameObject> buttons)
    {
        data.button.selectLevel[0].details.unLock = true;
        if (!DataManager.CheckJson()) DataManager.SaveToJson(data.button.selectLevel);
        DataManager.LoadFromJson(data.button.selectLevel);

        List<ButtonSelectLevel> levels = data.button.selectLevel;

        for (int i = 0; i < data.button.selectLevel.Count; i++)
        {
            InitializedButtonValue(buttons[i], levels[i]);
        }

        DataManager.DeployToData(data.button.selectLevel);
    }

    void InitializedButtonValue(GameObject button, ButtonSelectLevel level)
    {
        Color textColor = level.details.unLock ? Color.black : Color.white;

        button.GetComponentsInChildren<Text>()[0].text = level.indexLevel.ToString();
        button.GetComponentsInChildren<Text>()[0].color = textColor;
        button.GetComponentsInChildren<Text>()[1].text = level.details.unLock ? $"Score: {level.details.score}" : "";
        button.GetComponentsInChildren<Text>()[1].color = textColor;

        button.GetComponent<Image>().color = level.details.unLock ? Color.white : new Color32(65, 65, 65, 255);

        var mail = button.transform.Find("Mail icon").GetComponentsInChildren<Image>();

        if (level.details.mail != 0 && level.details.unLock)
        {
            for (int i = 0; i < level.details.mail; i++)
            {
                mail[i].color = Color.black;
            }
        }
        if (!level.details.unLock)
        {
            foreach (Image mailImage in mail)
            {
                mailImage.color = new Color32(70, 70, 70, 255);
            }
        }

        if (level.details.unLock) button.GetComponent<Button>().onClick.AddListener(() =>
        {
            ButtonEventManager.LoadScene(level.nameLevel);
        });
    }
}
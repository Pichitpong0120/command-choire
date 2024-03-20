using System.Collections.Generic;
using CommandChoice.Data;
using CommandChoice.Handler;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class ScoreBoardComponent : MonoBehaviour
    {
        [SerializeField] Button continueButton;
        [SerializeField] Button replaceButton;
        [SerializeField] Text scoreText;
        [SerializeField] Text timeText;
        [SerializeField] Text commandText;
        [SerializeField] List<GameObject> mail;

        void Start()
        {
            replaceButton.onClick.AddListener(() =>
            {
                GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand).GetComponent<CommandManager>().ResetAction();
                Destroy(gameObject);
            });
        }

        public void GetData()
        {
            PlayerManager player = GameObject.FindWithTag(StaticText.TagPlayer).GetComponent<PlayerManager>();
            CommandManager commandManager = GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand).GetComponent<CommandManager>();
            List<LevelSceneModel> scene = DataGlobal.Scene.ListLevelScene;
            bool newScore = false;
            for (int i = 0; i < DataGlobal.Scene.ListLevelScene.Count; i++)
            {
                if (scene[i].getNameForLoadScene() == SceneManager.GetActiveScene().name)
                {
                    foreach (var item in commandManager.ListCommandSelected)
                    {
                        if (item.name != StaticText.EndLoop) scene[i].DetailLevelScene.CountBoxCommand++;
                    }
                    commandText.text = scene[i].DetailLevelScene.CountBoxCommand.ToString();
                    scoreText.text = $"{scene[i].DetailLevelScene.ScoreLevelScene}%";
                    scene[i].DetailLevelScene.MailLevelScene = player.Mail;
                    scene[i].DetailLevelScene.UseTime = commandManager.TimeCount;
                    timeText.text = commandManager.TimeCount.ToString();
                    newScore = scene[i].DetailLevelScene.NewHightScore(100);
                    foreach (GameObject item in mail) { item.SetActive(false); }
                    for (int j = 0; j < scene[i].DetailLevelScene.MailLevelScene; j++)
                    {
                        mail[j].SetActive(true);
                    }
                    if (player.Mail > 0 && player.HP > 0)
                    {
                        try
                        {
                            scene[i + 1].DetailLevelScene.UnLockLevelScene = true;
                            continueButton.onClick.AddListener(() => SceneGameManager.LoadScene(scene[i + 1].getNameForLoadScene()));
                        }
                        catch (System.Exception)
                        {
                            continueButton.GetComponentInChildren<Text>().text = "Return to Select Levels";
                            continueButton.onClick.AddListener(() => SceneGameManager.LoadScene(SceneMenu.SelectLevel.ToString()));
                        }
                    }
                    else
                    {
                        continueButton.GetComponentInChildren<Text>().text = "Exit";
                        continueButton.onClick.AddListener(() => SceneGameManager.LoadScene(SceneMenu.SelectLevel.ToString()));
                    }
                    break;
                }
            }
        }
    }
}
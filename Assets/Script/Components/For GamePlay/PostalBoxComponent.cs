using System;
using System.Collections.Generic;
using CommandChoice.Data;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CommandChoice.Component
{
    public class PostalBoxComponent : MonoBehaviour
    {
        PlayerManager player;
        CommandManager commandManager;

        void Awake()
        {
            commandManager = GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand).GetComponent<CommandManager>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(StaticText.TagPlayer))
            {
                player = other.gameObject.GetComponent<PlayerManager>();
                commandManager.ResetAction(true);
                List<LevelSceneModel> scene = DataGlobal.Scene.ListLevelScene;
                bool newScore = false;
                for (int i = 0; i < DataGlobal.Scene.ListLevelScene.Count; i++)
                {
                    if (scene[i].getNameForLoadScene() == SceneManager.GetActiveScene().name)
                    {
                        scene[i].DetailLevelScene.CountBoxCommand = commandManager.ListCommandSelected.Count;
                        scene[i].DetailLevelScene.MailLevelScene = player.Mail;
                        scene[i].DetailLevelScene.UseTime = commandManager.TimeCount;
                        newScore = scene[i].DetailLevelScene.NewHightScore(100);
                        if (player.Mail > 0) scene[i + 1].DetailLevelScene.UnLockLevelScene = true;
                        ScoreBoardComponent gameObject = Instantiate(Resources.Load<ScoreBoardComponent>("Ui/Menu/Score Board"), GameObject.FindWithTag("Canvas").transform);
                        gameObject.GetData(scene[i].DetailLevelScene, newScore);
                    }
                }
            }
        }
    }
}
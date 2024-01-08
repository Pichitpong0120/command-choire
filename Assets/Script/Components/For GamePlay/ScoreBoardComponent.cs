using System.Collections.Generic;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class ScoreBoardComponent : MonoBehaviour
    {
        [SerializeField] Button replaceButton;
        [SerializeField] Text scoreText;
        [SerializeField] Text timeText;
        [SerializeField] Text commandText;
        [SerializeField] List<GameObject> mail;

        void Start()
        {
            replaceButton.onClick.AddListener(() =>
            {
                GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>().ResetAction();
                Destroy(gameObject);
            });
        }

        public void GetData(LevelSceneDetailModel levelSceneDetailModel, bool newScore)
        {
            foreach (GameObject item in mail) { item.SetActive(false); }
            scoreText.text = $"{levelSceneDetailModel.ScoreLevelScene}%";
            timeText.text = $"{levelSceneDetailModel.UseTime}";
            commandText.text = $"{levelSceneDetailModel.CountBoxCommand}";
            for (int i = 0; i < levelSceneDetailModel.MailLevelScene; i++)
            {
                mail[i].SetActive(true);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using CommandChoice.Data;
using CommandChoice.Handler;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class CommandManager : MonoBehaviour
    {
        [field: SerializeField] public DataGamePlay DataThisGame { get; private set; }
        [field: SerializeField] public int TimeCount { get; private set; } = 0;
        [field: SerializeField] public ListCommandModel ListCommandModel { get; private set; }
        [field: SerializeField] public Transform CommandContent { get; private set; }
        [SerializeField] private Text countTime;
        [field: SerializeField] public GameObject DropRemoveCommand { get; private set; }
        [field: SerializeField] public List<Transform> ListCommandSelected { get; private set; } = new();

        void Start()
        {
            DataThisGame = new();
            countTime.text = "";
        }

        public void RemoveCommand(GameObject command)
        {
            Destroy(command);
        }

        public void AddNewCommand(GameObject command)
        {
            command.transform.SetParent(CommandContent);
        }

        public void PlayAction(List<Transform> listCommand)
        {
            ListCommandSelected.Clear();
            LoopCheckCommand(listCommand);
            StartCoroutine(RunCommand(ListCommandSelected));
        }

        private void LoopCheckCommand(List<Transform> transformObject)
        {
            foreach (Transform parent in transformObject)
            {
                if (StaticText.CheckCommand(parent.gameObject.name)) ListCommandSelected.Add(parent);
                if (parent.childCount > 1)
                {
                    foreach (Transform child in parent)
                    {
                        LoopCheckCommand(child);
                    }
                }
            }
        }

        private void LoopCheckCommand(Transform transformObject)
        {
            if (StaticText.CheckCommand(transformObject.gameObject.name)) ListCommandSelected.Add(transformObject);
            if (transformObject.childCount > 1)
            {
                foreach (Transform child in transformObject)
                {
                    LoopCheckCommand(child);
                }
            }
        }

        public void ResetAction(bool stopCoroutinesOnly = false)
        {
            StopAllCoroutines();
            if (stopCoroutinesOnly) return;
            countTime.text = "";
            GameObject.FindGameObjectWithTag(StaticText.TagPlayer).GetComponent<PlayerManager>().ResetGame();
            foreach (GameObject item in DataThisGame.MailObjects)
            {
                if (item == null) break;
                item.GetComponent<MailComponent>().ResetGame();
            }
            foreach (Transform item in GameObject.Find("Right-Bottom").transform)
            {
                if (item.gameObject.name == "Run") item.gameObject.SetActive(true);
                if (item.gameObject.name == "Reset") item.gameObject.SetActive(false);
            }

            foreach (GameObject item in GameObject.FindGameObjectsWithTag(StaticText.TagCommand))
            {
                item.GetComponent<Command>().ResetAction();
            }
        }

        private IEnumerator RunCommand(List<Transform> listCommand)
        {
            TimeCount = 0;

            countTime.text = $"Count: {TimeCount}";
            PlayerManager player = GameObject.FindGameObjectWithTag(StaticText.TagPlayer).GetComponent<PlayerManager>();
            foreach (Transform item in listCommand)
            {
                yield return new WaitForSeconds(2f);
                if (listCommand.IndexOf(item) == 0)
                {
                    item.GetComponent<Command>().PlayingAction();
                }
                else
                {
                    Transform commandPerverse = listCommand[listCommand.IndexOf(item) - 1].transform;
                    commandPerverse.GetComponent<Command>().ResetAction();
                    item.GetComponent<Command>().PlayingAction();
                }
                if (item.name == StaticText.MoveUp)
                {
                    player.PlayerMoveUp();
                }
                else if (item.name == StaticText.MoveDown)
                {
                    player.PlayerMoveDown();
                }
                else if (item.name == StaticText.MoveLeft)
                {
                    player.PlayerMoveLeft();
                }
                else if (item.name == StaticText.MoveRight)
                {
                    player.PlayerMoveRight();
                }
                else if (item.name == StaticText.Loop)
                {
                    CommandAction.LoopAction(ListCommandSelected, listCommand.IndexOf(item));
                }
                //print(item);
                countTime.text = $"Count: {TimeCount += 1}";
            }
        }

        public void ConfigCommand(Command command, CommandFunction commandFunction)
        {
            if (command.Type != TypeCommand.Function) return;
            GameObject configPanels = Instantiate(Resources.Load<GameObject>(StaticText.PathPrefabConfigCommandForFunction), transform.root);
            ConfigCommand config = configPanels.GetComponent<ConfigCommand>();
            config.GetCommand(command, commandFunction);
        }

        public void TriggerCommand(Command command, CommandFunction commandFunction)
        {
            List<GameObject> l = new(GameObject.FindGameObjectsWithTag(StaticText.TagCommand));
            foreach (var item in l)
            {
                print(item.name);
            }
        }
    }
}
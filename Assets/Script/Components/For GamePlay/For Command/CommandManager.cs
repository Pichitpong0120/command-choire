using System.Collections;
using System.Collections.Generic;
using CommandChoice.Data;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class CommandManager : MonoBehaviour
    {
        [field: SerializeField] public DataGamePlay DataThisGame { get; private set; }
        [field: SerializeField] public int TimeCount { get; private set; } = 0;
        [field: SerializeField] public ListCommandModel ListCommand { get; private set; }
        [field: SerializeField] public Transform CommandContext { get; private set; }
        [SerializeField] private Text countTime;
        [field: SerializeField] public GameObject DropRemoveCommand { get; private set; }
        [field: SerializeField] public List<string> ListNameCommand { get; private set; } = new();

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
            command.transform.SetParent(CommandContext);
        }

        public void PlayAction(List<Transform> listCommand)
        {
            ListNameCommand.Clear();
            LoopCheckCommand(listCommand);
            StartCoroutine(RunCommand(ListNameCommand));
        }

        private void LoopCheckCommand(List<Transform> transformObject)
        {
            foreach (Transform parent in transformObject)
            {
                if (StaticText.CheckCommand(parent.gameObject.name)) ListNameCommand.Add(parent.gameObject.name);
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
            if (StaticText.CheckCommand(transformObject.gameObject.name)) ListNameCommand.Add(transformObject.gameObject.name);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ResetGame();
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
        }

        private IEnumerator RunCommand(List<string> listCommand)
        {
            TimeCount = 0;

            countTime.text = $"Count: {TimeCount}";
            PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            foreach (string item in listCommand)
            {
                yield return new WaitForSeconds(2f);
                if (item == StaticText.MoveUp)
                {
                    player.PlayerMoveUp();
                }
                else if (item == StaticText.MoveDown)
                {
                    player.PlayerMoveDown();
                }
                else if (item == StaticText.MoveLeft)
                {
                    player.PlayerMoveLeft();
                }
                else if (item == StaticText.MoveRight)
                {
                    player.PlayerMoveRight();
                }
                else if (item == StaticText.Loop)
                {
                    
                }
                //print(item);
                countTime.text = $"Count: {TimeCount += 1}";
            }
        }

        public void ConfigCommand(Command command, CommandFunction commandFunction)
        {
            if (command.Type != TypeCommand.Function) return;
            GameObject configPanels = Instantiate(Resources.Load<GameObject>("Ui/Command/Config Command Function"), transform.root);
            ConfigCommand config = configPanels.GetComponent<ConfigCommand>();
            config.GetCommand(command, commandFunction);
        }

        public void TriggerCommand(Command command, CommandFunction commandFunction)
        {
            List<GameObject> l = new(GameObject.FindGameObjectsWithTag("Command"));
            foreach (var item in l)
            {
                print(item.name);
            }
        }
    }
}
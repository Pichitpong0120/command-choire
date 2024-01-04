using System.Collections;
using System.Collections.Generic;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class CommandManager : MonoBehaviour
    {
        [field: SerializeField] public ListCommandModel ListCommand { get; private set; }
        [field: SerializeField] public Transform CommandContext { get; private set; }
        [SerializeField] private Text countTime;
        [field: SerializeField] public GameObject DropRemoveCommand { get; private set; }
        [SerializeField] private List<string> listNameCommand = new();

        void Start()
        {
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
            listNameCommand.Clear();
            LoopCheckCommand(listCommand);
            StartCoroutine(RunCommand(listNameCommand));
        }

        private void LoopCheckCommand(List<Transform> transformObject)
        {
            foreach (Transform parent in transformObject)
            {
                if (StaticText.CheckCommand(parent.gameObject.name)) listNameCommand.Add(parent.gameObject.name);
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
            if (StaticText.CheckCommand(transformObject.gameObject.name)) listNameCommand.Add(transformObject.gameObject.name);
            if (transformObject.childCount > 1)
            {
                foreach (Transform child in transformObject)
                {
                    LoopCheckCommand(child);
                }
            }
        }

        public void ResetAction()
        {
            StopAllCoroutines();
            countTime.text = "";
        }

        private IEnumerator RunCommand(List<string> listCommand)
        {
            int count = 0;

            countTime.text = $"Count: {count}";
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
                print(item);
                countTime.text = $"Count: {count += 1}";
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
            print("Triggering");
        }
    }
}
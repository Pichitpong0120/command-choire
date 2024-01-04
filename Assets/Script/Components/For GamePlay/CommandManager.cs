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

        public void PlayAction(List<GameObject> listCommand)
        {
            StartCoroutine(RunCommand(listCommand));
        }

        public void ResetAction()
        {
            StopAllCoroutines();
            countTime.text = "";
        }

        private IEnumerator RunCommand(List<GameObject> listCommand)
        {
            int count = 0;

            countTime.text = $"Count: {count}";
            PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            foreach (GameObject item in listCommand)
            {
                yield return new WaitForSeconds(2f);
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
                else { print("Wait a moment..."); }
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
    }
}
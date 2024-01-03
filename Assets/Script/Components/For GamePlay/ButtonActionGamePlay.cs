using System.Collections.Generic;
using CommandChoice.Data;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{

    public class ButtonActionGamePlay : MonoBehaviour
    {
        enum TypeAction
        {
            Play,
            Pause,
            Reset
        }

        [SerializeField] private TypeAction Type;

        private Button button;

        CommandManager commandManager;
        Transform listContentCommand;

        private void Awake()
        {
            button = gameObject.GetComponent<Button>();
            commandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
            listContentCommand = GameObject.FindGameObjectWithTag("List Content Command").transform;
        }

        private void Start()
        {
            if (Type == TypeAction.Play)
            {
                button.onClick.AddListener(() =>
                {
                    List<GameObject> listCommand = new();
                    SwitchActionPlay(false);
                    foreach (Transform child in listContentCommand)
                    {
                        listCommand.Add(child.gameObject);
                    }
                    commandManager.PlayAction(listCommand);
                });
            }
            else if (Type == TypeAction.Pause)
            {
                button.onClick.AddListener(() =>
                {
                    Time.timeScale = 0;
                    Instantiate(Resources.Load<GameObject>("Ui/Menu/PausePanels"), transform.root);
                });
            }
            else
            {
                button.onClick.AddListener(() =>
                {
                    SwitchActionPlay(true);
                    commandManager.ResetAction();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ResetGame();
                });
            }
        }

        private void SwitchActionPlay(bool action)
        {
            foreach (Transform child in transform.parent)
            {
                ButtonActionGamePlay actionGamePlay = child.GetComponent<ButtonActionGamePlay>();
                if (actionGamePlay.Type == TypeAction.Play)
                {
                    child.gameObject.SetActive(action);
                }
                else if (actionGamePlay.Type == TypeAction.Reset)
                {
                    child.gameObject.SetActive(!action);
                }
            }
        }
    }
}
using System.Collections.Generic;
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
            Reset,
            Zoom
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
                    List<Transform> listCommand = new();
                    SwitchActionPlay(false);
                    foreach (Transform child in listContentCommand)
                    {
                        listCommand.Add(child);
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
            else if (Type == TypeAction.Zoom)
            {
                ZoomComponent zoomComponent = gameObject.AddComponent<ZoomComponent>();
                zoomComponent.enabled = true;
            }
            else
            {
                button.onClick.AddListener(() =>
                {
                    ResetActionControl();
                });
            }
        }

        public void ResetActionControl()
        {
            SwitchActionPlay(true);
            commandManager.ResetAction();
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
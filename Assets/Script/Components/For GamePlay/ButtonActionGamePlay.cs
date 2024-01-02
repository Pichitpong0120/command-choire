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
            Reset
        }

        [SerializeField] private TypeAction Type;

        private Button button;

        private void Awake()
        {
            button = gameObject.GetComponent<Button>();
        }

        private void Start()
        {
            if (Type == TypeAction.Play)
            {
                button.onClick.AddListener(() =>
                {
                    List<GameObject> listCommand = new();
                    SwitchActionPlay(false);
                    CommandManager commandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
                    foreach (Transform child in GameObject.FindGameObjectWithTag("List Content Command").transform)
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
                });
            }
            else
            {
                button.onClick.AddListener(() =>
                {
                    SwitchActionPlay(true);
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
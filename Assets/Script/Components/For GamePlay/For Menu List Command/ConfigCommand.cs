using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class ConfigCommand : MonoBehaviour
    {
        [SerializeField] private Button buttonConfirm;
        [SerializeField] private Button buttonRemove;
        [SerializeField] private Button buttonClose;
        [SerializeField] private InputField configValue;
        [SerializeField] private Text textTitle;

        [SerializeField] private Command commandConfig;
        [SerializeField] private CommandFunction commandFunction;
        [SerializeField] private Text textConfig;
        [SerializeField] private CommandManager commandManager;

        void Awake()
        {
            commandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
        }

        void Start()
        {
            buttonConfirm.onClick.AddListener(() =>
            {
                int valueCount;
                try
                {
                    valueCount = int.Parse(configValue.text);
                }
                catch
                {
                    configValue.text = $"Need Number Only";
                    configValue.textComponent.color = Color.red;
                    return;
                }

                textConfig.text = CommandDisplay(commandConfig.gameObject.name);
                Destroy(gameObject);
            });

            buttonRemove.onClick.AddListener(() =>
            {
                commandManager.ListCommand.ReturnCommand(commandConfig);
                Destroy(commandConfig.gameObject);
                Destroy(gameObject);
            });

            buttonClose.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });

            configValue.onValueChanged.AddListener((string s) =>
            {
                configValue.textComponent.color = Color.black;
            });
        }

        public void GetCommand(Command command, CommandFunction commandFunction)
        {
            commandConfig = command;
            this.commandFunction = commandFunction;
            textConfig = commandFunction.gameObject.GetComponentInChildren<Text>();
        }

        public string CommandDisplay(string text)
        {

            string textUpdate = "";

            switch (text)
            {
                case StaticText.Idle:
                    textUpdate = text;
                    break;
                case StaticText.MoveUp:
                    textUpdate = text;
                    break;
                case StaticText.MoveDown:
                    textUpdate = text;
                    break;
                case StaticText.MoveLeft:
                    textUpdate = text;
                    break;
                case StaticText.MoveRight:
                    textUpdate = text;
                    break;
                case StaticText.Break:
                    textUpdate = text;
                    break;
                case StaticText.Count:
                    textUpdate = $"{text} : value";
                    break;
                case StaticText.If:
                    textUpdate = $"{text} : value";
                    break;
                case StaticText.Else:
                    textUpdate = text;
                    break;
                case StaticText.Loop:
                    textUpdate = $"{text} : Count value";
                    break;
                case StaticText.SkipTo:
                    textUpdate = $"{text} : value";
                    break;
                case StaticText.Trigger:
                    textUpdate = $"{text} : value";
                    break;
            }
            return textUpdate;
        }
    }
}

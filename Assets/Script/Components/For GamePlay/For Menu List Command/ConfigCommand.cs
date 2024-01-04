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
                commandFunction.count = valueCount;

                textConfig.text = StaticText.CommandDisplay(commandConfig.gameObject.name, commandFunction);
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
    }
}

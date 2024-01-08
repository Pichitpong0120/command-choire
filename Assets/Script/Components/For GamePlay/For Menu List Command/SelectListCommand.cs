using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class SelectListCommand : MonoBehaviour
    {
        [SerializeField] private Button ui;
        [SerializeField] private Button backBTN;
        [SerializeField] private Button behaviorType;
        [SerializeField] private Button functionType;
        [SerializeField] private GameObject parentCommandType;
        [SerializeField] private GameObject parentCommand;
        [SerializeField] private GameObject parentBehaviorCommand;
        [SerializeField] private GameObject parentFunctionCommand;
        [SerializeField] private GameObject MenuCommandBox;
        [SerializeField] private Transform transformBehavior;
        [SerializeField] private Transform transformFunction;
        CommandManager commandManager;

        void Awake()
        {
            parentCommandType.SetActive(true);
            parentCommand.SetActive(false);
            commandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
        }

        void Start()
        {
            behaviorType.onClick.AddListener(() =>
            {
                parentCommandType.SetActive(false);
                parentCommand.SetActive(true);
                parentBehaviorCommand.SetActive(true);
                parentFunctionCommand.SetActive(false);
            });

            functionType.onClick.AddListener(() =>
            {
                parentCommandType.SetActive(false);
                parentCommand.SetActive(true);
                parentBehaviorCommand.SetActive(false);
                parentFunctionCommand.SetActive(true);
            });

            backBTN.onClick.AddListener(() =>
            {
                parentCommandType.SetActive(true);
                parentCommand.SetActive(false);
            });

            ui.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });

            for (int i = 0; i < commandManager.ListCommand.commandBehavior.Count; i++)
            {
                if (commandManager.ListCommand.commandBehavior[i].Active)
                {
                    GenerateCommand(commandManager.ListCommand.commandBehavior[i], transformBehavior, TypeCommand.Behavior);
                }
            }

            for (int i = 0; i < commandManager.ListCommand.commandFunctions.Count; i++)
            {
                if (commandManager.ListCommand.commandFunctions[i].Active)
                {
                    GenerateCommand(commandManager.ListCommand.commandFunctions[i], transformFunction, TypeCommand.Function);
                }
            }
        }

        public void GenerateCommand(CommandModel nameCommand, Transform spawnCommand, TypeCommand type)
        {
            GameObject genCommandBox = Instantiate(MenuCommandBox, spawnCommand);

            genCommandBox.name = nameCommand.Name;
            genCommandBox.GetComponentInChildren<Text>().text = $"{nameCommand.Name} [{nameCommand.CanUse}]";
            genCommandBox.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (nameCommand.CanUse != 0)
                {
                    nameCommand.UsedCommand();
                    genCommandBox.GetComponentInChildren<Text>().text = nameCommand.Name;
                    genCommandBox.GetComponent<Button>().onClick.RemoveAllListeners();
                    Command setType = genCommandBox.AddComponent<Command>();
                    if (type == TypeCommand.Behavior)
                    {
                        setType.UpdateType(TypeCommand.Behavior);
                    }
                    else
                    {
                        setType.UpdateType(TypeCommand.Function);
                    }
                    setType.enabled = true;
                    commandManager.AddNewCommand(genCommandBox);
                    Destroy(gameObject);
                }
            });
        }
    }
}
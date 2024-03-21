using CommandChoice.Data;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class SelectListCommand : MonoBehaviour
    {
        [SerializeField] private Button uiParent;
        [SerializeField] private Button backBTN;
        [SerializeField] private Button buttonBehaviorType;
        [SerializeField] private Button buttonFunctionType;
        [SerializeField] private GameObject parentCommandType;
        [SerializeField] private GameObject parentCommand;
        [SerializeField] private GameObject parentBehaviorCommand;
        [SerializeField] private GameObject parentFunctionCommand;
        [SerializeField] private GameObject CommandPrefab;
        [SerializeField] private Transform transformBehavior;
        [SerializeField] private Transform transformFunction;
        CommandManager commandManager;

        void Awake()
        {
            parentCommandType.SetActive(true);
            parentCommand.SetActive(false);
            commandManager = GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand).GetComponent<CommandManager>();
            if (commandManager.ListCommandModel.CommandIsEmpty(0)) { buttonBehaviorType.gameObject.SetActive(false); }
            if (commandManager.ListCommandModel.CommandIsEmpty(1)) { buttonFunctionType.gameObject.SetActive(false); }
        }

        void Start()
        {
            buttonBehaviorType.onClick.AddListener(() =>
            {
                parentCommandType.SetActive(false);
                parentCommand.SetActive(true);
                parentBehaviorCommand.SetActive(true);
                parentFunctionCommand.SetActive(false);
            });

            buttonFunctionType.onClick.AddListener(() =>
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

            uiParent.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });

            for (int i = 0; i < commandManager.ListCommandModel.commandBehavior.Count; i++)
            {
                if (commandManager.ListCommandModel.commandBehavior[i].Active)
                {
                    GenerateCommand(commandManager.ListCommandModel.commandBehavior[i], transformBehavior, TypeCommand.Behavior);
                }
            }

            for (int i = 0; i < commandManager.ListCommandModel.commandFunctions.Count; i++)
            {
                if (commandManager.ListCommandModel.commandFunctions[i].Active)
                {
                    GenerateCommand(commandManager.ListCommandModel.commandFunctions[i], transformFunction, TypeCommand.Function);
                }
            }
        }

        public void GenerateCommand(CommandModel nameCommand, Transform spawnCommand, TypeCommand type)
        {
            GameObject genCommandBox = Instantiate(CommandPrefab, spawnCommand);

            genCommandBox.name = nameCommand.Name;
            genCommandBox.GetComponentInChildren<Text>().text = nameCommand.InfinityCanUse ? $"{nameCommand.Name}" : $"{nameCommand.Name} [{nameCommand.CanUse}]";
            genCommandBox.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (nameCommand.CanUse != 0 || nameCommand.InfinityCanUse)
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
                    commandManager.DataThisGame.percentScore -= DataGlobal.minusScoreBoxCommand;
                    Destroy(gameObject);
                }
            });
        }
    }
}
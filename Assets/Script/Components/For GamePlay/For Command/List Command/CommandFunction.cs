using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class CommandFunction : MonoBehaviour
    {
        [SerializeField] private CommandManager CommandManager;
        public GameObject RootContentCommand { get; private set; }
        [SerializeField] private GameObject commandFunction;
        public GameObject RootListViewCommand { get; private set; }

        public int count;

        public GameObject trigger;

        [SerializeField] private string nameCommand;

        void Awake()
        {
            RootContentCommand = GameObject.FindGameObjectWithTag("List Content Command");
            RootListViewCommand = GameObject.FindGameObjectWithTag("List View Command");
            CommandManager = RootListViewCommand.GetComponent<CommandManager>();
        }

        void Start()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            if (transform.parent.GetComponent<Command>() == null)
            {
                commandFunction = Instantiate(Resources.Load<GameObject>("Ui/Command/Block Command Function"), transform.parent);
                commandFunction.name = gameObject.name;
                nameCommand = commandFunction.name;
                gameObject.name = "Object Child";
                gameObject.tag = "Untagged";
                Command commandComponent = commandFunction.AddComponent<Command>();
                commandComponent.UpdateType(TypeCommand.Function);
                commandComponent.enabled = true;
                gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (StaticText.CheckCommandCanConfig(commandComponent.gameObject.name))
                    {
                        CommandManager.ConfigCommand(commandComponent, this);
                    }
                    else if (StaticText.CheckCommandCanTrigger(commandComponent.gameObject.name))
                    {
                        CommandManager.TriggerCommand(commandComponent, this);
                    }
                });

                Destroy(gameObject.GetComponent<Command>());
                StartConfigCommandFunction(commandComponent);
                transform.SetParent(commandComponent.transform);
                UpdateColor(RootContentCommand.transform);
            }
        }

        private void StartConfigCommandFunction(Command command)
        {
            if (command.gameObject.name == StaticText.Loop) CommandManager.ConfigCommand(command, this); ;
        }

        public void UpdateColor(Transform transform, bool revers = false)
        {
            int index = revers ? CommandManager.ListCommand.listColorCommands.Count - 1 : 1;
            foreach (Transform child in transform)
            {
                if (!StaticText.CheckCommandFunction(child.gameObject.name)) continue;
                foreach (Transform childInChild in child)
                {
                    if (childInChild.GetComponent<CommandFunction>() != null)
                    {
                        childInChild.GetComponent<Image>().color = CommandManager.ListCommand.listColorCommands[index];

                        if (child.childCount > 0)
                        {
                            foreach (Transform childInChildInChild in child.transform)
                            {
                                if (!StaticText.CheckCommandFunction(childInChildInChild.gameObject.name)) continue;
                                UpdateColor(childInChildInChild.transform, !revers);
                            }
                        }

                        if (revers)
                        {
                            _ = index > 1 ? index-- : index = CommandManager.ListCommand.listColorCommands.Count - 1;
                        }
                        else
                        {
                            _ = index < CommandManager.ListCommand.listColorCommands.Count - 1 ? index++ : index = 1;
                        }
                    }
                }
            }
        }
    }
}

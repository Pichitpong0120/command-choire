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

        public int countDefault;
        public int countTime;

        public GameObject triggerDefault;
        public GameObject trigger;

        [SerializeField] private string nameCommand;

        void Awake()
        {
            RootContentCommand = GameObject.FindGameObjectWithTag(StaticText.RootListContentCommand);
            RootListViewCommand = GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand);
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
                commandFunction = Instantiate(Resources.Load<GameObject>(StaticText.PathPrefabBlockCommandForFunction), transform.parent);
                commandFunction.name = gameObject.name;
                nameCommand = commandFunction.name;
                gameObject.name = StaticText.ObjectChild;
                gameObject.tag = StaticText.Untagged;
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

        public void UpdateTextCommand(string nameCommand, bool reset = false)
        {
            if(reset)
            {
                countTime = countDefault;
                trigger = triggerDefault;
            }
            Text command = transform.GetChild(0).GetComponent<Text>();
            command.text = StaticText.CommandDisplay(nameCommand, this);
        }

        public int UsedLoopCount(bool setNew = false)
        {
            if (setNew) countTime = countDefault;
            countTime -= 1;
            if(countTime < 0) countTime = 0;
            return countTime;
        }

        private void StartConfigCommandFunction(Command command)
        {
            if (command.gameObject.name == StaticText.Loop) CommandManager.ConfigCommand(command, this); ;
        }

        public void UpdateColor(Transform transform, bool revers = false)
        {
            int index = revers ? CommandManager.ListCommandModel.listColorCommands.Count - 1 : 1;
            foreach (Transform child in transform)
            {
                if (!StaticText.CheckCommandFunction(child.gameObject.name)) continue;
                foreach (Transform childInChild in child)
                {
                    if (childInChild.GetComponent<CommandFunction>() != null)
                    {
                        childInChild.GetComponent<Image>().color = CommandManager.ListCommandModel.listColorCommands[index];

                        if (revers)
                        {
                            _ = index > 1 ? index-- : index = CommandManager.ListCommandModel.listColorCommands.Count - 1;
                        }
                        else
                        {
                            _ = index < CommandManager.ListCommandModel.listColorCommands.Count - 1 ? index++ : index = 1;
                        }
                    }
                    if (StaticText.CheckCommandFunction(childInChild.gameObject.name))
                    {
                        UpdateColor(child.transform, !revers);
                    }
                }
            }
        }
    }
}

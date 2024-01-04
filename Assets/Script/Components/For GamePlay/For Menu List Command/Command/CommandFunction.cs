using CommandChoice.Component;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

public class CommandFunction : MonoBehaviour
{
    [SerializeField] private CommandManager CommandManager;
    public GameObject RootContentCommand { get; private set; }
    [SerializeField] private GameObject commandFunction;
    private RectTransform RectTransform;
    public GameObject RootListViewCommand { get; private set; }

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        CommandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
        RootContentCommand = GameObject.FindGameObjectWithTag("List Content Command");
        RootListViewCommand = GameObject.FindGameObjectWithTag("List View Command");
    }

    void Start()
    {
        InitCommand();
        UpdateColor(RootContentCommand.transform);
    }

    private void InitCommand()
    {
        if (transform.parent.GetComponent<Command>() == null)
        {

            commandFunction = Instantiate(Resources.Load<GameObject>("Ui/Command/Block Command Function"), transform.parent);
            commandFunction.name = gameObject.name;
            gameObject.name = "Object Child";
            Command commandComponent = commandFunction.AddComponent<Command>();
            commandComponent.UpdateType(TypeCommand.Function);
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                RootListViewCommand.GetComponent<CommandManager>().ConfigCommand(commandComponent, this);
            });

            Destroy(gameObject.GetComponent<Command>());
            StartConfigCommandFunction(commandComponent);
            transform.SetParent(commandComponent.transform);
        }
    }

    private void StartConfigCommandFunction(Command command)
    {
        if (command.gameObject.name == StaticText.Loop) RootListViewCommand.GetComponent<CommandManager>().ConfigCommand(command, this); ;
    }

    public void UpdateColor(Transform transform, bool revers = false)
    {
        int index = revers ? CommandManager.ListCommand.listColorCommands.Count - 1 : 1;
        foreach (Transform child in transform)
        {
            foreach (Transform childInChild in child)
            {
                if (childInChild.GetComponent<CommandFunction>() != null)
                {
                    childInChild.GetComponent<Image>().color = CommandManager.ListCommand.listColorCommands[index];

                    if (child.childCount > 0)
                    {
                        foreach (Transform childInChildInChild in child.transform)
                        {
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

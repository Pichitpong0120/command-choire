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

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        CommandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
        RootContentCommand = GameObject.FindGameObjectWithTag("List Content Command");
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
            Command commandComponent = commandFunction.AddComponent<Command>();
            commandComponent.UpdateType(TypeCommand.Function);

            Destroy(gameObject.GetComponent<Command>());

            transform.SetParent(commandComponent.transform);
        }
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

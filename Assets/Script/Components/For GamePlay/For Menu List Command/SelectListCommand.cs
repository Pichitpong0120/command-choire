using CommandChoice.Component;
using CommandChoice.Handler;
using UnityEngine;
using UnityEngine.UI;

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
            if (commandManager.ListCommand.commandBehavior[i].active)
            {
                GameObject gameObject = Instantiate(MenuCommandBox, transformBehavior);
                gameObject.GetComponentInChildren<Text>().text = commandManager.ListCommand.commandBehavior[i].NameCommand.name;
            }
        }

        for (int i = 0; i < commandManager.ListCommand.commandFunctions.Count; i++)
        {
            if (commandManager.ListCommand.commandFunctions[i].active)
            {
                GameObject gameObject = Instantiate(MenuCommandBox, transformFunction);
                gameObject.GetComponentInChildren<Text>().text = commandManager.ListCommand.commandFunctions[i].NameCommand.name;
            }
        }
    }

    public void SelectCommand(GameObject clickedObject)
    {
        // Transform parentListCommand = GameObject.FindGameObjectWithTag("List Content Command").transform;
        // string nameObject = clickedObject.name;

        // foreach(CommandBehaviorModel command in commandModel.commandBehavior)
        // {
        //     if(nameObject == command.commandObject.name)
        //     {
        //         SceneGameManager.SpawnGameObject(command.commandObject, parentListCommand);
        //         Destroy(this.gameObject);
        //     }
        // }

        // foreach(CommandFunctionModel command in commandModel.commandFunctions)
        // {
        //     if(nameObject == command.commandObject.name)
        //     {
        //         SceneGameManager.SpawnGameObject(command.commandObject, parentListCommand);
        //         Destroy(this.gameObject);
        //     }
        // }
    }
}
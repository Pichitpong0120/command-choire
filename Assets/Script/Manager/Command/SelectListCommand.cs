using UnityEngine;
using UnityEngine.UI;

public class SelectListCommand : MonoBehaviour
{
    [SerializeField] private Button ui;
    [SerializeField] private Button backBTN;
    [SerializeField] private GameObject parentCommandType;
    [SerializeField] private Button behaviorType;
    [SerializeField] private Button functionType;
    [SerializeField] private GameObject parentCommand;
    [SerializeField] private GameObject parentBehaviorCommand;
    [SerializeField] private GameObject parentFunctionCommand;
    [SerializeField] private CommandModel commandModel;

    void Start()
    {
        SelectCommandType();
    }

    private void SelectCommandType()
    {
        behaviorType.onClick.AddListener(() => {
            parentCommandType.SetActive(false);
            parentCommand.SetActive(true);
            parentBehaviorCommand.SetActive(true);
            parentFunctionCommand.SetActive(false);
        });

        functionType.onClick.AddListener(() => {
            parentCommandType.SetActive(false);
            parentCommand.SetActive(true);
            parentBehaviorCommand.SetActive(false);
            parentFunctionCommand.SetActive(true);
        });

        backBTN.onClick.AddListener(() => {
            parentCommandType.SetActive(true);
            parentCommand.SetActive(false);
        });

        ui.onClick.AddListener(() => {
            Destroy(this.gameObject);
        });
    }

    public void SelectCommand(GameObject clickedObject)
    {
        Transform parentListCommand = GameObject.FindGameObjectWithTag("List Content Command").transform;
        string nameObject = clickedObject.name;
        
        foreach(CommandBehaviorModel command in commandModel.commandBehavior)
        {
            if(nameObject == command.commandObject.name)
            {
                SceneGameManager.SpawnGameObject(command.commandObject, parentListCommand);
                Destroy(this.gameObject);
            }
        }
        
        foreach(CommandFunctionModel command in commandModel.commandFunctions)
        {
            if(nameObject == command.commandObject.name)
            {
                SceneGameManager.SpawnGameObject(command.commandObject, parentListCommand);
                Destroy(this.gameObject);
            }
        }
    }
}
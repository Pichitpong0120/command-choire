using UnityEngine;
using UnityEngine.UI;

public class CommandBehaviorDisplay : MonoBehaviour
{
    public Command infoCommand {get; private set;}
    private Button button;

    [field:SerializeField] public Text label {get; private set;}
    [SerializeField] private GameObject objectPrefab;

    [field:SerializeField] public int count {get; set;} = 0;

    void Awake()
    {
        infoCommand = GetComponentInChildren<Command>();
        button = GetComponentInChildren<Button>();
    }

    void Start()
    {
        UpdateText(count.ToString());
        button.onClick.AddListener(() => {
            GameObject cloneConfig = Instantiate(objectPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
            cloneConfig.GetComponent<ConfigCommand>().GetCommand(gameObject);
        });
    }

    public void UpdateText(string text)
    {
        int.TryParse(text, out int result);
        
        count = result;

        string textUpdate = "";

        switch (infoCommand.GetCommand())
        {
            case "Stop":
                textUpdate = $"Stop : {text} Turn";
                break;
            case "MoveRight":
                textUpdate =  $"Move Right : {text} Turn";
                break;
            case "MoveLeft":
                textUpdate =  $"Move Left : {text} Turn";
                break;
            case "Jump":
                textUpdate =  $"Jump : {text} Turn";
                break;
            case "JumpRight":
                textUpdate =  $"Jump Right : {text} Turn";
                break;
            case "JumpLeft":
                textUpdate =  $"Jump Left : {text} Turn";
                break;
            case "ClimbUp":
                textUpdate =  $"Climb Up : {text} Turn";
                break;
            case "ClimbDown":
                textUpdate =  $"Climb Down : {text} Turn";
                break;
        }

        label.text = textUpdate;
    }
}

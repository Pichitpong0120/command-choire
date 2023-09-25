using UnityEngine;
using UnityEngine.UI;

public class CommandStop : MonoBehaviour
{
    public Command infoCommand {get; private set;}
    private Button button;

    [field:SerializeField] public Text label {get; private set;}
    [SerializeField] private GameObject objectPrefab;

    public int count {get; set;} = 0;

    void Awake()
    {
        infoCommand = GetComponentInChildren<Command>();
        button = GetComponentInChildren<Button>();
    }

    void Start()
    {
        label.text = UpdateText(count.ToString());
        button.onClick.AddListener(() => {
            GameObject cloneConfig = Instantiate(objectPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
            cloneConfig.GetComponent<ConfigCommand>().GetCommand(gameObject);
        });
    }

    public string UpdateText(string text)
    {
        int.TryParse(text, out int result);
        count = result;
        return $"Stop : {text} Turn";
    }
}

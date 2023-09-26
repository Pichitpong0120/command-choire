using UnityEngine;
using UnityEngine.UI;

public class ConfigCommand : MonoBehaviour
{
    [SerializeField] private Button buttonConfirm;
    [SerializeField] private Button buttonRemove;
    [SerializeField] private Button buttonClose;
    [SerializeField] private InputField configValue;
    [SerializeField] private Text textTitle;

    private GameObject gameCommandObject;
    private string checkCommand;

    void Start()
    {
        buttonConfirm.onClick.AddListener(() => {
            int valueCount;
            try
            {
                valueCount = int.Parse(configValue.text);
            }
            catch
            {
                configValue.textComponent.color = Color.red;
                return;
            }

            CommandBehaviorDisplay command = gameCommandObject.GetComponent<CommandBehaviorDisplay>();
            command.UpdateText(configValue.text);

            Destroy(this.gameObject);
        });

        buttonRemove.onClick.AddListener(() => {
            Destroy(gameCommandObject);
            Destroy(this.gameObject);
        });

        buttonClose.onClick.AddListener(() => {
            Destroy(this.gameObject);
        });

        configValue.onValueChanged.AddListener((string s) => {
            configValue.textComponent.color = Color.black;
        });
    }
    
    public void GetCommand(GameObject commandObject)
    {
        gameCommandObject = commandObject;
        checkCommand = gameCommandObject.GetComponentInChildren<Command>().GetCommand();
        textTitle.text = $"Command : {checkCommand}";
    }
}

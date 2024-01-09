using CommandChoice.Component;
using UnityEngine;
using UnityEngine.UI;

public class CommandBehavior : MonoBehaviour
{
    [SerializeField] private Image ColorBackground;

    [SerializeField] private CommandManager CommandManager;

    void Awake()
    {
        ColorBackground = GetComponent<Image>();
        CommandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
    }

    void Start()
    {
        ColorBackground.color = CommandManager.ListCommand.listColorCommands[0];
    }
}

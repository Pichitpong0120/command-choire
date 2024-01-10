using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class CommandBehavior : MonoBehaviour
    {
        [SerializeField] private Image ColorBackground;

        [SerializeField] private CommandManager CommandManager;

        void Awake()
        {
            ColorBackground = GetComponent<Image>();
            CommandManager = GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand).GetComponent<CommandManager>();
        }

        void Start()
        {
            ColorBackground.color = CommandManager.ListCommandModel.listColorCommands[0];
        }
    }
}

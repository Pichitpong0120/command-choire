using UnityEngine;
using UnityEngine.EventSystems;

namespace CommandChoice.Component
{
    public class RemoveCommand : MonoBehaviour, IDropHandler
    {
        [SerializeField] private CommandManager commandManager;

        void Awake()
        {
            commandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
        }

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            Command dropCommandObject = eventData.pointerDrag.GetComponent<Command>();

            commandManager.ListCommand.ReturnCommand(dropCommandObject);
            Destroy(dropCommandObject.gameObject);
            gameObject.SetActive(false);
        }
    }
}

using CommandChoice.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class RemoveCommand : MonoBehaviour, IDropHandler
    {
        [Header("Field Auto Set")]
        [SerializeField] private CommandManager commandManager;
        [SerializeField] private GameObject commandContent;
        [SerializeField] private ContentSizeFitter contentSize;
        [SerializeField] private VerticalLayoutGroup verticalLayout;

        void Awake()
        {
            commandManager = GameObject.FindGameObjectWithTag(StaticText.RootListViewCommand).GetComponent<CommandManager>();
            commandContent = GameObject.FindGameObjectWithTag(StaticText.RootListContentCommand);
            contentSize = commandContent.GetComponent<ContentSizeFitter>();
            verticalLayout = commandContent.GetComponent<VerticalLayoutGroup>();
        }

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            Command dropCommandObject = eventData.pointerDrag.GetComponent<Command>();
            contentSize.enabled = true;
            verticalLayout.enabled = true;
            commandManager.ListCommandModel.ReturnCommand(dropCommandObject);
            Destroy(dropCommandObject.gameObject);
            gameObject.SetActive(false);
        }
    }
}

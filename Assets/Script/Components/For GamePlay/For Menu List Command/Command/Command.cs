using System;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class Command : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public float speedScroll;
        [field: SerializeField] public TypeCommand Type { get; private set; }
        [SerializeField] private ParentCommand Parent = new();

        public GameObject RootContentCommand { get; private set; }
        public CommandManager CommandManager { get; private set; }

        private bool OnDrag = false;
        private PointerEventData eventData;
        private Vector3 beginDrag = new();
        [SerializeField] private GameObject removeCommand;

        void Awake()
        {
            RootContentCommand = GameObject.FindGameObjectWithTag("List Content Command");
            CommandManager = GameObject.FindGameObjectWithTag("List View Command").GetComponent<CommandManager>();
        }

        void Start()
        {

            removeCommand = GameObject.FindGameObjectWithTag("Remove Command");
            Parent.UpdateParentAndIndex(transform.parent, transform.GetSiblingIndex());
            if (Type != TypeCommand.Null)
            {
                if (Type == TypeCommand.Behavior)
                {
                    gameObject.AddComponent<CommandBehavior>();
                }
                else
                {
                    if (transform.GetChild(0).GetComponent<CommandFunction>() == null)
                    {
                        gameObject.AddComponent<CommandFunction>();
                    }
                }
            }
        }

        void Update()
        {
            if (OnDrag)
            {
                Vector2 transformRoot = RootContentCommand.GetComponent<RectTransform>().anchoredPosition;
                float updateListView = Time.deltaTime * (beginDrag.y - eventData.pointerDrag.transform.position.y);
                if (beginDrag.y <= eventData.pointerDrag.transform.position.y)
                {
                    transformRoot.y -= Math.Abs(updateListView);
                }
                else if (beginDrag.y >= eventData.pointerDrag.transform.position.y)
                {
                    transformRoot.y += Math.Abs(updateListView);
                }
                RootContentCommand.GetComponent<RectTransform>().anchoredPosition = transformRoot;
                //print(eventData.pointerDrag.transform.position.y - beginDrag.y);
            }
        }

        public void UpdateType(TypeCommand typeCommand)
        {
            Type = typeCommand;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"OnPointerDown '{gameObject.name}'");
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log($"OnBeginDrag '{gameObject.name}'");
            this.eventData = eventData;
            Parent.UpdateParentAndIndex(transform.parent, transform.GetSiblingIndex());
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            GetComponent<Image>().raycastTarget = false;
            GetComponentInChildren<Text>().raycastTarget = false;
            GetComponent<Button>().enabled = false;
            OnDrag = true;
            CommandManager.DropRemoveCommand.SetActive(OnDrag);
            beginDrag = transform.position;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Debug.Log($"OnDrag '{gameObject.name}'");
            transform.position = Input.mousePosition;
            //print(RootContentCommand.transform.position.y - eventData.pointerDrag.transform.position.y);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"OnEndDrag '{gameObject.name}'");
            transform.SetParent(Parent.parent);
            transform.SetSiblingIndex(Parent.index);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            GetComponent<Image>().raycastTarget = true;
            GetComponentInChildren<Text>().raycastTarget = true;
            GetComponent<Button>().enabled = true;
            OnDrag = false;
            CommandManager.DropRemoveCommand.SetActive(OnDrag);
            if (Type == TypeCommand.Function)
            {
                CommandFunction command = transform.GetChild(0).GetComponent<CommandFunction>();
                command.UpdateColor(command.RootContentCommand.transform);
            }
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            Command dropCommandObject = eventData.pointerDrag.GetComponent<Command>();

            if (Type == TypeCommand.Function)
            {
                if (dropCommandObject.transform.position.y - transform.position.y >= 80)
                {
                    dropCommandObject.Parent.UpdateIndex(transform.GetSiblingIndex());
                    dropCommandObject.Parent.UpdateParent(transform.parent);
                }
                else if (dropCommandObject.transform.position.y - transform.position.y <= -60)
                {
                    dropCommandObject.Parent.UpdateIndex(transform.GetSiblingIndex() + 1);
                    dropCommandObject.Parent.UpdateParent(transform.parent);
                }
                else
                {
                    dropCommandObject.Parent.UpdateParentAndIndex(transform, 1);
                }
            }
            else
            {
                if (transform.position.y >= dropCommandObject.transform.position.y)
                {
                    dropCommandObject.Parent.UpdateIndex(transform.GetSiblingIndex() + 1);
                }
                else
                {
                    dropCommandObject.Parent.UpdateIndex(transform.GetSiblingIndex());
                }
                dropCommandObject.Parent.UpdateParent(transform.parent);
            }

            Debug.Log($"'{dropCommandObject.name}' OnDrop '{gameObject.name}'");
        }
    }
}
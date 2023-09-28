using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Command : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    enum CommandType
    {
        Behavior,
        Function,
    }

    enum ListCommandActive
    {
        MoveLeft,
        MoveRight,
        Jump,
        JumpLeft,
        JumpRight,
        Stop,
        ClimbUp,
        ClimbDown,
        Loop,
        Break,
        If,
        Else,
        Count,
        SkipTo,
        Trigger
    }

    enum SetParentTransform
    {
        ParentTransform,
        parentLocationTransform
    }

    [SerializeField] private CommandType commandType;
    [SerializeField] private ListCommandActive listCommand;

    private Transform parentLocationBeforeDrag;
    public int indexInParent;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(commandType == CommandType.Behavior)
        {
            parentLocationBeforeDrag = GetParentTransform(SetParentTransform.parentLocationTransform);
            indexInParent = GetParentTransform().GetSiblingIndex();
            GetParentTransform().SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            GetComponent<Image>().raycastTarget = false;
            GetComponentInChildren<Text>().raycastTarget = false;
            GetComponent<Button>().enabled = false;
            Debug.Log("OnBeginDrag");
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(commandType == CommandType.Behavior)
        {
            GetParentTransform().position = Input.mousePosition;
        }
        else
        {
            transform.position  = Input.mousePosition;
        }
        Debug.Log("OnDrag");
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GameObject gameObjectDrop = eventData.pointerDrag;
        if(transform.parent != null)
        {
            if(transform.parent.position.y >= gameObjectDrop.transform.parent.position.y)
            {
                gameObjectDrop.GetComponent<Command>().indexInParent = transform.parent.GetSiblingIndex() + 1;
            }
            else
            {
                gameObjectDrop.GetComponent<Command>().indexInParent = transform.parent.GetSiblingIndex();
            }
        }
        Debug.Log("OnDrop");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if(commandType == CommandType.Behavior)
        {
            GetParentTransform().SetParent(parentLocationBeforeDrag);
            GetParentTransform().SetSiblingIndex(indexInParent);
            GetComponent<Button>().enabled = true;
            GetComponent<Image>().raycastTarget = true;
            GetComponentInChildren<Text>().raycastTarget = true;
        }
        Debug.Log("OnEndDrag");
    }

    private Transform GetParentTransform(SetParentTransform set = SetParentTransform.ParentTransform)
    {
        Transform newTransform = transform;
        for (int i = 0; i <= set.GetHashCode(); i++)
        {
            newTransform = newTransform.parent;
        }
        return newTransform;
    }

    public string GetCommandType()
    {
        return commandType.ToString();
    }

    public string GetCommand()
    {
        return listCommand.ToString();
    }
}
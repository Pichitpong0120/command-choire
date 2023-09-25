using UnityEngine;
using UnityEngine.EventSystems;

public class Command : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    enum CommandType{
            Behavior,
            Function,
        }

    enum ListCommandActive{
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

    [SerializeField] private CommandType commandType;
    [SerializeField] private ListCommandActive listCommand;
    private Canvas canvas;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.localPosition = eventData.delta / canvas.scaleFactor;
        Debug.Log("OnDrag");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
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
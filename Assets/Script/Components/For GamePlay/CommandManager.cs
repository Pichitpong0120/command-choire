using CommandChoice.Model;
using UnityEngine;

namespace CommandChoice.Component
{
    public class CommandManager : MonoBehaviour
    {
        [field: SerializeField] public ListCommandModel ListCommand { get; private set; }
        [field: SerializeField] public Transform CommandContext { get; private set; }

        public void RemoveCommand(GameObject command)
        {
            Destroy(command);
        }

        public void addNewCommand(GameObject command)
        {
            command.transform.SetParent(CommandContext);
        }

        public void MoveCommand(GameObject command)
        {
            int currentSiblingIndex = command.transform.GetSiblingIndex();
            int lastIndex = command.transform.parent.childCount - 1;

            if (currentSiblingIndex != lastIndex)
            {
                command.transform.SetSiblingIndex(lastIndex);
            }
        }
    }
}
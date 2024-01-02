using System.Collections;
using System.Collections.Generic;
using CommandChoice.Model;
using UnityEngine;

namespace CommandChoice.Component
{
    public class CommandManager : MonoBehaviour
    {
        [field: SerializeField] public ListCommandModel ListCommand { get; private set; }
        [field: SerializeField] public Transform CommandContext { get; private set; }

        [SerializeField] private GameObject ObjectEntry;

        void Update()
        {
            ObjectEntry.transform.SetAsLastSibling();
        }

        public void RemoveCommand(GameObject command)
        {
            Destroy(command);
        }

        public void AddNewCommand(GameObject command)
        {
            command.transform.SetParent(CommandContext);
        }

        public void PlayAction(List<GameObject> listCommand)
        {
            StartCoroutine(RunCommand(listCommand));
        }

        private IEnumerator RunCommand(List<GameObject> listCommand)
        {
            foreach (GameObject item in listCommand)
            {
                print(item.name);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
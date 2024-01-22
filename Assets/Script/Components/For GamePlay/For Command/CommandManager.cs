using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommandChoice.Data;
using CommandChoice.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class CommandManager : MonoBehaviour
    {
        [field: SerializeField] public DataGamePlay DataThisGame { get; private set; }
        [field: SerializeField] public int TimeCount { get; private set; } = 0;
        [field: SerializeField] public ListCommandModel ListCommandModel { get; private set; }
        [field: SerializeField] public Transform CommandContent { get; private set; }
        [SerializeField] private Text countTime;
        [field: SerializeField] public GameObject DropRemoveCommand { get; private set; }
        [field: SerializeField] public List<Transform> ListCommandSelected { get; private set; } = new();
        private readonly List<LoopCommandModel> LoopCommands = new();

        void Start()
        {
            DataThisGame = new();
            countTime.text = "";
        }

        public void RemoveCommand(GameObject command)
        {
            Destroy(command);
        }

        public void AddNewCommand(GameObject command)
        {
            command.transform.SetParent(CommandContent);
        }

        public void PlayAction(List<Transform> listCommand)
        {
            ListCommandSelected.Clear();
            LoopCheckCommand(listCommand);
            StartCoroutine(RunCommand(ListCommandSelected));
        }

        private void LoopCheckCommand(List<Transform> transformObject)
        {
            foreach (Transform parent in transformObject)
            {
                if (StaticText.CheckCommand(parent.gameObject.name)) ListCommandSelected.Add(parent);
                if (parent.childCount > 1)
                {
                    foreach (Transform child in parent)
                    {
                        LoopCheckCommand(child);
                    }
                }
                if (parent.gameObject.name == StaticText.Loop)
                {
                    Transform newObject = Resources.Load<GameObject>(StaticText.PathPrefabCommand).transform;
                    newObject.name = StaticText.EndLoop;
                    ListCommandSelected.Add(newObject);
                };
            }
        }

        private void LoopCheckCommand(Transform transformObject)
        {
            if (StaticText.CheckCommand(transformObject.gameObject.name)) ListCommandSelected.Add(transformObject);
            if (transformObject.childCount > 1)
            {
                foreach (Transform child in transformObject)
                {
                    LoopCheckCommand(child);
                }
            }
            if (transformObject.gameObject.name == StaticText.Loop)
            {
                Transform newObject = Resources.Load<GameObject>(StaticText.PathPrefabCommand).transform;
                newObject.name = StaticText.EndLoop;
                ListCommandSelected.Add(newObject);
            };
        }

        public void ResetAction(bool stopCoroutinesOnly = false)
        {
            StopAllCoroutines();
            if (stopCoroutinesOnly) return;
            countTime.text = "";
            LoopCommands.Clear();
            foreach (Transform item in ListCommandSelected)
            {
                Command command1 = item.GetComponent<Command>();
                if (command1 == null) continue;
                CommandFunction command2 = command1.CommandFunction;
                if (command2 == null) continue;
                command2.UpdateTextCommand(item.name, true);
            }
            GameObject.FindGameObjectWithTag(StaticText.TagPlayer).GetComponent<PlayerManager>().ResetGame();
            foreach (GameObject item in DataThisGame.MailObjects)
            {
                if (item == null) break;
                item.GetComponent<MailComponent>().ResetGame();
            }
            foreach (Transform item in GameObject.Find("Right-Bottom").transform)
            {
                if (item.gameObject.name == "Run") item.gameObject.SetActive(true);
                if (item.gameObject.name == "Reset") item.gameObject.SetActive(false);
            }

            foreach (GameObject item in GameObject.FindGameObjectsWithTag(StaticText.TagCommand))
            {
                item.GetComponent<Command>().ResetAction();
            }
        }

        private IEnumerator RunCommand(List<Transform> listCommand, int? timeSet = null, bool IsChild = false)
        {
            if (timeSet == null) TimeCount = 0;
            bool CheckSetEndLoop = true;

            countTime.text = $"Count: {TimeCount}";
            PlayerManager player = GameObject.FindGameObjectWithTag(StaticText.TagPlayer).GetComponent<PlayerManager>();
            foreach (var item in listCommand.Select((value, index) => new { value, index }))
            {
                if (item.index == 0)
                {
                    yield return new WaitForSeconds(2f);
                    item.value.GetComponent<Command>().PlayingAction();
                }
                else if (item.value.name == StaticText.EndLoop)
                {
                    yield return new WaitForSeconds(1f);
                    if (CheckSetEndLoop) LoopCommands[0].SetEndLoop(item.index);
                    int index = item.index;
                    Transform commandPerverse = listCommand[index - 1].transform;
                    while (commandPerverse.name == StaticText.EndLoop)
                    {
                        commandPerverse = listCommand[index -= 1].transform;
                    }
                    if (commandPerverse.name != StaticText.EndLoop) commandPerverse.GetComponent<Command>().ResetAction();
                    if (LoopCommands.Count == 0) continue;
                    if (LoopCommands[0].CommandTransform.GetComponent<Command>().CommandFunction.countTime == 0)
                    {
                        print($"Remove End Loop: {LoopCommands.Count} index Start: {LoopCommands.First().IndexStartLoop} index End: {LoopCommands.First().IndexEndLoop} count command: {LoopCommands[0].CommandTransform.GetComponent<Command>().CommandFunction.countTime}");
                        LoopCommands.RemoveAt(0);
                        continue;
                    }
                    if (LoopCommands.Count > 1)
                    {
                        if (!CheckSetEndLoop && LoopCommands[1].CommandTransform.GetComponent<Command>().CommandFunction.countTime == 0)
                        {
                            continue;
                        }
                    }
                    print($"Still End Loop: {LoopCommands.Count} index Start: {LoopCommands.First().IndexStartLoop} index End: {LoopCommands.First().IndexEndLoop} count command: {LoopCommands[0].CommandTransform.GetComponent<Command>().CommandFunction.countTime}");
                    yield return StartCoroutine(RunCommand(LoopCommands.First().CheckCommand(ListCommandSelected), TimeCount, true));
                    continue;
                }
                else
                {
                    yield return new WaitForSeconds(2f);
                    int index = item.index;
                    Transform commandPerverse = listCommand[index - 1].transform;
                    while (commandPerverse.name == StaticText.EndLoop)
                    {
                        commandPerverse = listCommand[index -= 1].transform;
                    }
                    commandPerverse.GetComponent<Command>().ResetAction();
                    if (item.value.name != StaticText.EndLoop)
                    {
                        item.value.GetComponent<Command>().PlayingAction();
                    }
                }
                if (item.value.name == StaticText.MoveUp)
                {
                    player.PlayerMoveUp();
                }
                else if (item.value.name == StaticText.MoveDown)
                {
                    player.PlayerMoveDown();
                }
                else if (item.value.name == StaticText.MoveLeft)
                {
                    player.PlayerMoveLeft();
                }
                else if (item.value.name == StaticText.MoveRight)
                {
                    player.PlayerMoveRight();
                }
                else if (item.value.name == StaticText.Loop)
                {
                    Command command = item.value.GetComponent<Command>();
                    if (LoopCommands.Count > 0)
                    {
                        if (LoopCommands[0].CommandTransform.GetHashCode() == item.value.GetHashCode() && IsChild)
                        {
                            CheckSetEndLoop = false;
                            command.CommandFunction.UsedLoopCount();
                            command.CommandFunction.UpdateTextCommand(item.value.name);
                            countTime.text = $"Count: {TimeCount += 1}";
                            print($"Still Start Loop: {LoopCommands.Count} index Start: {LoopCommands.First().IndexStartLoop} index End: {LoopCommands.First().IndexEndLoop}  count command: {command.CommandFunction.countTime}");
                            continue;
                        }
                    }
                    if (command.CommandFunction.countTime > 0)
                    {
                        CheckSetEndLoop = true;
                        command.CommandFunction.UsedLoopCount();
                        command.CommandFunction.UpdateTextCommand(item.value.name);
                        LoopCommands.Insert(0, new LoopCommandModel(item.value, item.index));
                        print($"Add Start Loop: {LoopCommands.Count} index Start: {LoopCommands.First().IndexStartLoop} index End: {LoopCommands.First().IndexEndLoop} count command: {command.CommandFunction.countTime}");
                    }
                }
                countTime.text = $"Count: {TimeCount += 1}";
            }
            print("End Run");
        }

        public void ConfigCommand(Command command, CommandFunction commandFunction)
        {
            if (command.Type != TypeCommand.Function) return;
            GameObject configPanels = Instantiate(Resources.Load<GameObject>(StaticText.PathPrefabConfigCommandForFunction), transform.root);
            ConfigCommand config = configPanels.GetComponent<ConfigCommand>();
            config.GetCommand(command, commandFunction);
        }

        public void TriggerCommand(Command command, CommandFunction commandFunction)
        {
            List<GameObject> l = new(GameObject.FindGameObjectsWithTag(StaticText.TagCommand));
            foreach (var item in l)
            {
                print(item.name);
            }
        }
    }
}
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
            List<Transform> Output = new List<Transform>();

            Stack<int> loopIndexStack = new Stack<int>(); // เก็บ index ของลูป "for"
            Stack<int> loopCountStack = new Stack<int>(); // เก็บจำนวนครั้งที่ต้องทำซ้ำของลูป "for"

            for (int i = 0; i < ListCommandSelected.Count; i++)
            {
                Transform command = ListCommandSelected[i];

                if (command.name.ToLower().StartsWith("loop"))
                {
                    int loopCount = command.GetComponent<Command>().CommandFunction.countDefault;
                    loopIndexStack.Push(Output.Count); // เก็บ index ปัจจุบันของลูป "for"
                    loopCountStack.Push(loopCount); // เก็บจำนวนครั้งที่ต้องทำซ้ำของลูป "for"
                    Output.Add(command); // เพิ่มคำสั่ง "for" ในเอาต์พุต
                }
                else if (command.name.ToLower().StartsWith("end"))
                {
                    int loopIndex = loopIndexStack.Pop(); // ดึง index ของลูป "for" ที่ตรงกับ "end"
                    int loopCount = loopCountStack.Pop(); // ดึงจำนวนครั้งที่ต้องทำซ้ำของลูป "for"
                    List<Transform> loopContent = new List<Transform>(); // สร้างรายการสำหรับเก็บเนื้อหาของลูป "for"
                                                                         // ดึงเนื้อหาของลูป "for" จากเอาต์พุต
                    for (int j = loopIndex; j < Output.Count; j++)
                    {
                        loopContent.Add(Output[j]);
                    }
                    // นำเนื้อหาของลูป "for" มาเพิ่มในเอาต์พุตตามจำนวนครั้งที่ต้องทำซ้ำ
                    for (int k = 1; k < loopCount; k++)
                    {
                        Output.AddRange(loopContent);
                    }
                }
                else
                {
                    Output.Add(command); // เพิ่มคำสั่ง move ในเอาต์พุต
                }
            }

            // foreach (Transform item in Output)
            // {
            //     Debug.Log(item.name);
            // }

            StartCoroutine(RunCommand(Output));
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

            countTime.text = $"Count: {TimeCount}";
            PlayerManager player = GameObject.FindGameObjectWithTag(StaticText.TagPlayer).GetComponent<PlayerManager>();
            foreach (var item in listCommand.Select((value, index) => new { value, index }))
            {
                yield return new WaitForSeconds(2f);
                try
                {
                    listCommand[item.index - 1].GetComponent<Command>().ResetAction();
                    listCommand[item.index].GetComponent<Command>().PlayingAction();
                }
                catch (System.Exception)
                {
                    listCommand[item.index].GetComponent<Command>().PlayingAction();
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
                    if (item.value.childCount > 1)
                    {
                        CheckLoopForCountTextUI(item.value);
                    }
                    command.CommandFunction.UsedLoopCount();
                    command.CommandFunction.UpdateTextCommand(item.value.name);
                }
                countTime.text = $"Count: {TimeCount += 1}";
            }
            print("End Run");
        }

        void CheckLoopForCountTextUI(Transform transformParent)
        {
            foreach (Transform item in transformParent)
            {
                if (item.name == StaticText.Loop)
                {
                    if (item.GetComponent<Command>().CommandFunction.countTime <= 0)
                    {
                        CommandFunction checkCommandLoop = item.GetComponent<Command>().CommandFunction;
                        checkCommandLoop.countTime = checkCommandLoop.countDefault;
                        checkCommandLoop.UpdateTextCommand(item.name);
                    }
                    if (item.transform.childCount > 1)
                    {
                        CheckLoopForCountTextUI(item.transform);
                    }
                }
            }
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
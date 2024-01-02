using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CommandChoice.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "ListCommand", menuName = "ScriptableObject/ListCommand", order = 0)]
    public class ListCommandModel : ScriptableObject
    {
        [SerializeField]
        public List<CommandModel> commandBehavior;

        [SerializeField]
        public List<CommandModel> commandFunctions;
        [SerializeField]
        public List<Color32> listColorCommands;

        ListCommandModel()
        {
            commandBehavior = new List<CommandModel>(){
                new(StaticText.MoveUp),
                new(StaticText.MoveDown),
                new(StaticText.MoveLeft),
                new(StaticText.MoveRight),
                new(StaticText.Idle),
            };

            commandFunctions = new List<CommandModel>(){
                new(StaticText.Break),
                new(StaticText.Count),
                new(StaticText.If),
                new(StaticText.Else),
                new(StaticText.Loop),
                new(StaticText.SkipTo),
                new(StaticText.Trigger),
            };

            listColorCommands = new List<Color32>(){
                new(244, 132, 132, 255),
                new(255, 0, 102, 255),
                new(255, 0, 0, 255),
                new(0, 176, 80, 255)
            };
        }
    }

    [Serializable]
    public class ParentCommand
    {
        public Transform parent;
        public int index = 0;

        public void UpdateParentAndIndex(Transform TransformParent, int IndexInParent)
        {
            parent = TransformParent;
            index = IndexInParent;
        }

        public void UpdateParent(Transform TransformParent)
        {
            parent = TransformParent;
        }

        public void UpdateIndex(int IndexInParent)
        {
            index = IndexInParent;
        }
    }

    [Serializable]
    public class CommandModel
    {
        [field: SerializeField] public bool Active { get; private set; } = false;

        [field: SerializeField] public string Name { get; private set; } = "Command";

        [field: SerializeField] public int CanUse { get; private set; } = 0;

        public CommandModel(string NameCommand)
        {
            Name = NameCommand;
        }

        public void UsedCommand()
        {
            CanUse -= 1;
        }
    }

    public enum TypeCommand
    {
        Null,
        Behavior,
        Function
    }
}



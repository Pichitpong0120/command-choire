using System;
using System.Collections.Generic;
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
    }


    [Serializable]
    public class CommandModel
    {
        [SerializeField]
        public bool active = false;

        [SerializeField]
        public GameObject NameCommand;

        [SerializeField]
        public int CanUseHowMuch = 0;
    }

}

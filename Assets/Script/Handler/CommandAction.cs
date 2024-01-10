using System.Collections.Generic;
using UnityEngine;

namespace CommandChoice.Handler
{
    public class CommandAction
    {
        public static void LoopAction(List<Transform> listCommandSelected, int indexStart)
        {
            Debug.Log(listCommandSelected[indexStart].name);
        }
    }
}
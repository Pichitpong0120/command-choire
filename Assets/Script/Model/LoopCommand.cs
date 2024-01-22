using System.Collections.Generic;
using CommandChoice.Component;
using UnityEngine;

namespace CommandChoice.Model
{
    public class LoopCommandModel
    {
        public Transform CommandTransform;
        public int IndexStartLoop { get; private set; } = 0;
        public int IndexEndLoop { get; private set; } = 0;

        public LoopCommandModel(Transform item, int indexStartLoop)
        {
            CommandTransform = item;
            IndexStartLoop = indexStartLoop;
        }

        public void SetEndLoop(int indexEndLoop)
        {
            IndexEndLoop = indexEndLoop;
        }

        public List<Transform> CheckCommand(List<Transform> dataCommand)
        {
            List<Transform> newData = new();
            bool init = true;
            for (int i = 0; i < dataCommand.Count; i++)
            {
                if (i >= IndexStartLoop && i <= IndexEndLoop) 
                {
                    if(!init && dataCommand[i].name == StaticText.Loop)
                    {
                        dataCommand[i].GetComponent<Command>().CommandFunction.UpdateTextCommand(dataCommand[i].name, true);
                    }
                    newData.Add(dataCommand[i]);
                    //Debug.Log(dataCommand[i]);
                    init =false;
                }
            }
            return newData;
        }
    }
}
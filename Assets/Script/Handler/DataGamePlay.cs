using System.Collections.Generic;
using CommandChoice.Model;
using UnityEngine;

namespace CommandChoice.Data
{
    public class DataGamePlay
    {
        public List<GameObject> MailObjects { get; private set; } = new();
        public List<GameObject> EnemyObjects { get; private set; } = new();

        public DataGamePlay()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(StaticText.TagMail))
            {
                if (item == null) break;
                MailObjects.Add(item);
            };
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(StaticText.TagEnemy))
            {
                if (item == null) break;
                EnemyObjects.Add(item);
            };
        }
    }
}
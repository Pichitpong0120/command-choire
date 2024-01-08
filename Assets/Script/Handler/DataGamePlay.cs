using System.Collections.Generic;
using UnityEngine;

namespace CommandChoice.Data
{
    public class DataGamePlay
    {
        public List<GameObject> MailObjects { get; private set; } = new();
        public List<GameObject> EnemyObjects { get; private set; } = new();

        public DataGamePlay()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mail"))
            {
                if (item == null) break;
                MailObjects.Add(item);
            };
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (item == null) break;
                EnemyObjects.Add(item);
            };
        }
    }
}
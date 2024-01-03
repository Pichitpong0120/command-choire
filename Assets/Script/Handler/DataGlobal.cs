using System;
using CommandChoice.Model;
using UnityEngine;

namespace CommandChoice.Data
{
    class DataGlobal
    {
        public static SceneModel Scene { get; private set; } = new("Assets/Scene/Level");
        public static bool PauseGame = false;
    }
}

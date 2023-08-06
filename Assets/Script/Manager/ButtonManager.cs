using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ButtonManager
{
    [field: SerializeField] public List<ButtonLoadScene> loadScene { get; private set;}
    [field: SerializeField] public List<ButtonSelectLevel> selectLevel { get; private set;}
}

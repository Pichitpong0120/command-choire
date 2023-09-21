using System;
using UnityEngine;

[Serializable]
public class CommandBehaviorModel
{
    [field: SerializeField] public GameObject commandObject {get; private set;}
}
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonLoadScene
{
    [field: SerializeField] public Button buttonObject  { get; set; }
    [field: SerializeField] public string nameScene  { get; set; }
    [field: SerializeField] public bool active  { get; set; } = true;
}
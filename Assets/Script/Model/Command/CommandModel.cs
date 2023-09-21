using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ListCommand", menuName = "ScriptableObject/ListCommand", order = 0)]
public class CommandModel : ScriptableObject {
    public List<CommandBehaviorModel> commandBehavior;
    public List<CommandFunctionModel> commandFunctions;
}
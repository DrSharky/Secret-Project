using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "CommonCompCommands")]
public class CommonCompCommands : SerializedScriptableObject
{
    public enum Commands
    {
        Help,
        Quit,
        List
    }
    public Dictionary<Commands, ComputerCommand> commandDict = new Dictionary<Commands, ComputerCommand>()
    {
        { Commands.Help, new ComputerCommand("help") },
        { Commands.Quit, new ComputerCommand("quit") },
        { Commands.List, new ComputerCommand("list") }
    };

}
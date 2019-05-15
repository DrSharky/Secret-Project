using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Menu Command List")]
public class MenuCommandList : SerializedScriptableObject
{
    [SerializeField]
    public List<CompMenuCommand> Commands;
}
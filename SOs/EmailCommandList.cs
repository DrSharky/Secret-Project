using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Email Command List")]
public class EmailCommandList : SerializedScriptableObject
{
    [SerializeField]
    public List<EmailCommand> Commands;
}
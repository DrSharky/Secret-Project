using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Email Command List")]
public class EmailCommandList : SerializedScriptableObject
{
    public bool hasEmail;
    public string accountName;
    public string password;

    [SerializeField]
    public List<EmailCommand> Commands;

    public int GetEmailCount()
    {
        return Commands.Count;
    }

    public int GetUnreadCount()
    {
        return Commands.FindAll(x => !x.read).Count;
    }
}
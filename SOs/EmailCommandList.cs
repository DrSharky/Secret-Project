using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

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
        return Commands.Where(f => f.showEmail).ToList().Count;
    }

    public List<EmailCommand> GetDisplayEmails()
    {
        return Commands.Where((f) => f.showEmail).ToList();
    }

    public int GetUnreadCount()
    {
        return Commands.FindAll(x => !x.read).Count;
    }
}
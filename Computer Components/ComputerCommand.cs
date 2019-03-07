using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ComputerCommand
{
    [SerializeField]
    public string commandText;

    [TextArea]
    public string displayText;
    public MessageType messageType;

    public ComputerCommand(string name)
    {
        commandText = name;
    }

    public ComputerCommand()
    {
        commandText = "home";
        messageType = MessageType.Menu;
    }
}

[Serializable]
public class MenuCommand : ComputerCommand
{
    public List<ComputerCommand> subCommands;

    public string menuTitle;
    public string menuSubtitle;

    [TextArea]
    public string commandsDisplayText;

    public string menuPanelTitle;
    public bool hackable;
    public bool alreadyHacked;
    public string password;

    public MenuCommand(string name) : base(name)
    {
        messageType = MessageType.Menu;
        subCommands = new List<ComputerCommand>();
    }

    public MenuCommand() : base()
    {
        commandText = "home";
        messageType = MessageType.Menu;
    }
}

[Serializable]
public class EmailCommand : ComputerCommand
{
    public bool read;

    protected bool questRelated;
    public string sender;
    public string subject;

    public EmailCommand() : base()
    {
        messageType = MessageType.Email;
    }
}

[Serializable]
public class EmailMenuCommand : MenuCommand
{
    public List<EmailCommand> emailCommands;

    public EmailMenuCommand()
    {
        commandText = "email";
        messageType = MessageType.EmailMenu;
        hackable = true;
    }

    public void AssignEmails(List<EmailCommand> listOfEmails)
    {
        emailCommands = listOfEmails;
    }
}

public enum MessageType
{
    None,
    Email,
    Quest,
    Menu,
    EmailMenu,
    Misc,
    Warning
}

//Class to store some email info.
//Might expand this into another class & file later.
//[Serializable]
//public class EmailInfo
//{
//    [HideInInspector]
//    public int totalEmails = 0;
//    [HideInInspector]
//    public int unreadEmails = 0;
//    public bool hasEmail = false;
//    public string emailPassword;
//    public string accountName;
//}
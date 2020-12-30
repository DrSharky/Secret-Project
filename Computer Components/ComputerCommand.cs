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
    public CompMessageType messageType;

    public ComputerCommand(string name)
    {
        commandText = name;
    }

    public ComputerCommand()
    {
        //commandText = "home";
        //messageType = CompMessageType.Menu;
    }
}

[Serializable]
public class CompMenuCommand : ComputerCommand
{
    public List<ComputerCommand> subCommands;

    public string menuTitle;
    public string menuSubtitle;

    [TextArea]
    public string commandsDisplayText;
    public string menuPanelTitle;
    public bool hackable;
    //public bool alreadyHacked;
    public string password;

    public CompMenuCommand(string name) : base(name)
    {
        messageType = CompMessageType.Menu;
        subCommands = new List<ComputerCommand>();
    }

    public CompMenuCommand() : base()
    {
        //commandText = "home";
        messageType = CompMessageType.Menu;
    }
}

[Serializable]
public class EmailCommand : ComputerCommand
{
    //public bool read;
    //public bool showEmail;

    protected bool questRelated;
    public string sender;
    public string subject;

    public EmailCommand() : base()
    {
        messageType = CompMessageType.Email;
    }
}

[Serializable]
public class EmailMenuCommand : CompMenuCommand
{
    public List<EmailCommand> emailCommands;

    public EmailMenuCommand()
    {
        commandText = StringManager.emailCmd;
        messageType = CompMessageType.EmailMenu;
        hackable = true;
    }

    public void AssignEmails(List<EmailCommand> listOfEmails)
    {
        emailCommands = listOfEmails;
    }
}

public enum CompMessageType
{
    None,
    Email,
    Quest,
    Menu,
    EmailMenu,
    Misc,
    Warning
}
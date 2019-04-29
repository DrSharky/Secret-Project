using System.Collections.Generic;

public class CommonCompStrings
{
    #region Enums

    public enum Help
    {
        Text,
        Title
    }

    public enum Error
    {
        Text,
        Title
    }

    public enum Misc
    {
        MenuTitleSuffix,
        Alphabet,
    }

    public enum Email
    {
        Prefix,
        TitleYou,
        TitleNum,
        TitleUnread,
        SubjectDisplay,
        SenderDisplay
    }

    public enum Command
    {
        List,
        Email,
        MenuIndent,
        Help,
        Quit
    }

    public enum Input
    {
        PassHeader,
        CmdHeader,
        Continue
    }

    public enum Password
    {
        Required,
        Success,
        Fail,
        Accepted,
        Entering
    }

    public enum Char
    {
        NewLine,
        Greater,
        Empty,
        LBracket,
        RBracket,
        Period
    }

    #endregion

    #region Dictionaries

    
    public static Dictionary<Error, string> errorDict = new Dictionary<Error, string>
    {
        {Error.Text, "Type \"list\" to get the available commands \nType \"help\" for assistance." },
        {Error.Title, "Invalid command: " }
    };

    public static Dictionary<Help, string> helpDict = new Dictionary<Help, string>
    {
        { Help.Text,
            "Type \"list\" to get the available menus and commands.\n" +
            "Menu names are listed in brackets.Type the name of a menu to switch to that menu.\n" +
            "Command names are listed after menu names.Type the name of a command to run that command.\n" +
            "Different menus have different available commands."
        },
        { Help.Title, "Help information" }
    };

    public static Dictionary<Misc, string> miscDict = new Dictionary<Misc, string>
    {
        { Misc.MenuTitleSuffix, " menu" },
        { Misc.Alphabet, "abcdefghijklmnopqrstuvwxyz" }
    };

    public static Dictionary<Email, string> emailDict = new Dictionary<Email, string>
    {
        { Email.Prefix, "Email For " },
        { Email.TitleYou, "You have " },
        { Email.TitleNum, " emails, " },
        { Email.TitleUnread, " are unread." },
        { Email.SubjectDisplay, "(Subject) " },
        { Email.SenderDisplay, "(From) " }
    };

    public static Dictionary<Command, string> cmdDict = new Dictionary<Command, string>
    {
        { Command.List, "list" },
        { Command.Email, "email" },
        { Command.MenuIndent, "   " },
        { Command.Help, "help" },
        { Command.Quit, "quit" }
    };

    public static Dictionary<Input, string> inputDict = new Dictionary<Input, string>
    {
        { Input.PassHeader, "Password : " },
        { Input.CmdHeader, "Type menu or command: " },
        { Input.Continue, "[Press \"ENTER\" to continue]"}
    };

    public static Dictionary<Password, string> passDict = new Dictionary<Password, string>
    {
        { Password.Required, "Password required" },
        { Password.Success, "Password Succeeded" },
        { Password.Fail, "PASSWORD FAILED" },
        { Password.Accepted, "Password accepted: <" },
        { Password.Entering, "Entering " }
    };

    public static Dictionary<Char, string> charDict = new Dictionary<Char, string>
    {
        { Char.NewLine, "\n" },
        { Char.Greater, ">" },
        { Char.Empty, "" },
        { Char.LBracket, "[" },
        { Char.RBracket, "]" },
        { Char.Period, "." }
    };

    #endregion
}
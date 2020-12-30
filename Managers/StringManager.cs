using UnityEngine;

public static class StringManager
{
    #region EventManager
    public const string displayPanelToggle = "DisplayPanel Event ";
    public const string menuPanelToggle = "MenuPanel Event ";
    public const string emailPanelToggle = "EmailPanel Event ";
    public const string exitScreenEvent = "ExitScreen Event ";
    public const string passwordPanelToggle = "PassPanel Event ";
    public const string titlePanelToggle = "TitlePanel Event ";
    #endregion

    #region Computer

    #region Computer Menu
    public const string menuTitleSuffix = " menu";
    #endregion

    #region Computer Command
    public const string homeCmd = "home";
    public const string listCmd = "list";
    public const string emailCmd = "email";
    public const string menuIndent = "   ";
    public const string helpCmd = "help";
    public const string quitCmd = "quit";
    #endregion

    #region Computer Command Display
    public const string passHeaderText = "Password : ";
    public const string commandHeaderText = "Type menu or command: ";
    public const string enterToContinue = "[Press \"ENTER\" to continue]";
    #endregion

    #region Computer Password
    public const string passReq = "Password required";
    public const string passSucc = "Password Succeeded";
    public const string passFail = "Password Failed";
    public const string displayPassAccepted = "Password accepted: <";
    public const string entering = "Entering ";
    #endregion

    #region Computer Error
    public const string errorText = "Type \"list\" to get the available menus and commands.\n"+
                                    "Menu names are listed in brackets.Type the name of a menu to switch to that menu.\n"+
                                    "Command names are listed after menu names.Type the name of a command to run that command.\n"+
                                    "Different menus have different available commands.";
    public const string helpTitle = "Help information";
    #endregion

    #region Computer Misc
    public const string alpha = "abcdefghijklmnopqrstuvwxyz";
    #endregion

    #region Computer Email
    public const string emailPrefix = "Email For ";
    public const string emailTitleYou = "You have ";
    public const string emailTitleNum = " emails, ";
    public const string emailTitleUnread = " are unread.";
    #endregion

    #endregion

    #region Miscellaneous
    public const string newLine = "\n";
    public const string grthan = ">";
    public const string emptyString = "";
    public const string leftBr = "[";
    public const string rightBr = "]";
    public const string period = ".";
    #endregion

}
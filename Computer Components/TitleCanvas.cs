using UnityEngine;
using UnityEngine.UI;

//TODO: Work in progress. Figure out how to include string changes in game events.
public class TitleCanvas : MonoBehaviour
{
    public Text titleText;
    public Text subtitleText;
    public MenuCommandList menuCmdList;
    public EmailCommandList emailInfo;

    private CanvasGroup canvasGroup;
    private MenuCommand currentMenu;
    private string errorCmd;

    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SwitchMenu(string menu)
    {
        currentMenu = menuCmdList.Commands.Find(x => x.commandText == menu);
        SwitchState(ScreenType.Menu);
    }

    public void ErrorCommand(string command)
    {
        errorCmd = command;
    }

    private void ResetSubtitle()
    {
        if (subtitleText.text != CommonCompStrings.charDict[CommonCompStrings.Char.Empty])
            subtitleText.text = CommonCompStrings.charDict[CommonCompStrings.Char.Empty];
    }

    //Could edit this to make menu event a special case where it only passes string instead
    //Title is always showing, so no need to make another case for it.
    //Same kinda goes for other menu events. Just use the specific menu events,
    //along with the generic menu event. specific takes care of strings, generic takes care of on/off.
    //Maybe this would work?
    public void SwitchState(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.Password:
                titleText.text = CommonCompStrings.passDict[CommonCompStrings.Password.Required];
                ResetSubtitle();
                break;
            case ScreenType.PasswordFail:
                titleText.text = CommonCompStrings.passDict[CommonCompStrings.Password.Fail];
                ResetSubtitle();
                break;
            case ScreenType.PasswordSucceed:
                titleText.text = CommonCompStrings.passDict[CommonCompStrings.Password.Success];
                ResetSubtitle();
                break;
            case ScreenType.Menu:
            case ScreenType.DisplayText:
                titleText.text = currentMenu.menuTitle;
                subtitleText.text = currentMenu.menuSubtitle;
                break;
            case ScreenType.Email:
            case ScreenType.EmailMenu:
                titleText.text = CommonCompStrings.emailDict[CommonCompStrings.Email.Prefix] + emailInfo.accountName;
                ResetSubtitle();
                break;
            case ScreenType.Help:
                titleText.text = CommonCompStrings.helpDict[CommonCompStrings.Help.Title];
                ResetSubtitle();
                break;
            case ScreenType.Error:
                titleText.text = CommonCompStrings.errorDict[CommonCompStrings.Error.Title] + errorCmd;
                ResetSubtitle();
                    break;
            case ScreenType.None:
            default:
                break;
        }
    }
}
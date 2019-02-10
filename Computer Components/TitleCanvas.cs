using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Work in progress. Figure out how to include string changes in game events.
public class TitleCanvas : MonoBehaviour
{
    public Text titleText;
    public Text subtitleText;
    public CommonCompStrings commonStrings;
    public MenuCommandList menuCmdList;

    private CanvasGroup canvasGroup;
    private MenuCommand currentMenu;
    private EmailCommandList emailInfo;
    private string errorCmd;

    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SwitchMenu(MenuCommand menu)
    {
        currentMenu = menu;
    }

    public void ErrorCommand(string command)
    {
        errorCmd = command;
    }

    private void ResetSubtitle()
    {
        if (subtitleText.text != commonStrings.charDict[CommonCompStrings.Char.Empty])
            subtitleText.text = commonStrings.charDict[CommonCompStrings.Char.Empty];
    }

    public void SwitchState(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.Password:
                titleText.text = commonStrings.passDict[CommonCompStrings.Password.Required];
                ResetSubtitle();
                break;
            case ScreenType.PasswordFail:
                titleText.text = commonStrings.passDict[CommonCompStrings.Password.Fail];
                ResetSubtitle();
                break;
            case ScreenType.PasswordSucceed:
                titleText.text = commonStrings.passDict[CommonCompStrings.Password.Success];
                ResetSubtitle();
                break;
            case ScreenType.Menu:
            case ScreenType.DisplayText:
                titleText.text = currentMenu.menuTitle;
                subtitleText.text = currentMenu.menuSubtitle;
                break;
            case ScreenType.Email:
            case ScreenType.EmailMenu:
                titleText.text = commonStrings.emailDict[CommonCompStrings.Email.Prefix] + emailInfo.accountName;
                break;
            case ScreenType.Help:
                titleText.text = commonStrings.helpDict[CommonCompStrings.Help.Title];
                ResetSubtitle();
                break;
            case ScreenType.Error:
                titleText.text = commonStrings.errorDict[CommonCompStrings.Error.Title] + errorCmd;
                    break;
        }
    }
}
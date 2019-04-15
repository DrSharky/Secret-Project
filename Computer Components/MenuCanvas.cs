using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public delegate void MenuDelegate(string value);
    public event MenuDelegate OnMenuSwitch;

    public MenuCommandList menuCmdList;
    public EmailCommandList emailInfo;

    public UnityEngine.UI.Text menuTitleText;
    public UnityEngine.UI.Text menuListText;
    public UnityEngine.UI.Text commandListText;
    public UnityEngine.UI.Text emailTitleText;
    private CanvasGroup canvasGroup;
    private MenuCommand currentMenu;

    private string emailText;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        SetEmailText();
    }

    public void SwitchMenu(string menu)
    {
        currentMenu = menuCmdList.Commands.Find(x => x.commandText == menu);
        menuTitleText.text = currentMenu.menuPanelTitle;
        menuListText.text = currentMenu.displayText;
        commandListText.text = currentMenu.commandsDisplayText;
        OnMenuSwitch(menu);
        SetEmailText();
    }

    public void SetEmailText()
    {
        emailText = CommonCompStrings.emailDict[CommonCompStrings.Email.TitleYou] + emailInfo.GetEmailCount() +
            CommonCompStrings.emailDict[CommonCompStrings.Email.TitleNum] +
            emailInfo.GetUnreadCount() + CommonCompStrings.emailDict[CommonCompStrings.Email.TitleUnread];
        emailTitleText.text = emailText;
    }

    public void SwitchState(ScreenType screenType)
    {
        switch(screenType)
        {
            case ScreenType.DisplayText:
            case ScreenType.EmailMenu:
            case ScreenType.Email:
            case ScreenType.Password:
            case ScreenType.PasswordFail:
            case ScreenType.PasswordSucceed:
            case ScreenType.None:
            case ScreenType.Help:
            case ScreenType.Error:
            default:
                canvasGroup.alpha = 0;
                break;
            case ScreenType.Menu:
                canvasGroup.alpha = 1;
                break;

        }
    }
}
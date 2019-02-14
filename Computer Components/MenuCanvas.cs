using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public MenuCommandList menuCmdList;

    public UnityEngine.UI.Text menuTitleText;
    private CanvasGroup canvasGroup;
    private MenuCommand currentMenu;

    public CommonCompStrings commonStrings;

    public void SwitchMenu(string menu)
    {
        currentMenu = menuCmdList.Commands.Find(x => x.commandText == menu);
        menuTitleText.text = currentMenu.menuPanelTitle;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCanvas : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public MenuCommandList menuCmdList;
    private CompMenuCommand currentMenu;
    private ComputerCommand currentCommand;
    public UnityEngine.UI.Text displayText;
    public MenuCanvas menuCanvas;

    public void SwitchDisplay(string cmd)
    {
        displayText.text = currentMenu.subCommands.Find(x => x.commandText == cmd).displayText;
    }

    public void SwitchMenu(string menu)
    {
        currentMenu = menuCmdList.Commands.Find(x => x.commandText == menu);
    }

	// Use this for initialization
	void Start()
	{
        canvasGroup = GetComponent<CanvasGroup>();
        menuCanvas.OnMenuSwitch += SwitchMenu;
	}

    public void SwitchState(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.DisplayText:
                canvasGroup.alpha = 1;
                break;
            case ScreenType.Help:
                canvasGroup.alpha = 1;
                displayText.text = CommonCompStrings.helpDict[CommonCompStrings.Help.Text];
                break;
            case ScreenType.Error:
                canvasGroup.alpha = 1;
                displayText.text = CommonCompStrings.errorDict[CommonCompStrings.Error.Text];
                break;
            case ScreenType.Menu:
                canvasGroup.alpha = 0;
                break;
            case ScreenType.Password:
                canvasGroup.alpha = 0;
                break;
            case ScreenType.Email:
                canvasGroup.alpha = 0;
                break;
            case ScreenType.EmailMenu:
                canvasGroup.alpha = 0;
                break;
            case ScreenType.PasswordFail:
                canvasGroup.alpha = 1;
                break;
            case ScreenType.PasswordSucceed:
                canvasGroup.alpha = 1;

                displayText.text = CommonCompStrings.passDict[CommonCompStrings.Password.Accepted] +
                            currentMenu.password +
                            CommonCompStrings.charDict[CommonCompStrings.Char.Greater] +
                            CommonCompStrings.charDict[CommonCompStrings.Char.NewLine] +
                            CommonCompStrings.passDict[CommonCompStrings.Password.Entering] +
                            currentMenu.commandText +
                            CommonCompStrings.charDict[CommonCompStrings.Char.Period];
                break;
            case ScreenType.None:
                canvasGroup.alpha = 0;
                break;
            default:
                break;
        }
    }
}
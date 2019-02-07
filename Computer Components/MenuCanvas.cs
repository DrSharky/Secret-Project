using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SwitchState(ComputerGameEvent.ScreenType screenType)
    {
        switch(screenType)
        {
            case ComputerGameEvent.ScreenType.DisplayText:
            case ComputerGameEvent.ScreenType.EmailMenu:
            case ComputerGameEvent.ScreenType.Email:
            case ComputerGameEvent.ScreenType.Password:
            case ComputerGameEvent.ScreenType.PasswordFail:
            case ComputerGameEvent.ScreenType.None:
            default:
                canvasGroup.alpha = 0;
                break;
            case ComputerGameEvent.ScreenType.Normal:
                canvasGroup.alpha = 1;
                break;

        }
    }
}
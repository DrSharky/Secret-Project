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

    public void SwitchState(ScreenType screenType)
    {
        switch(screenType)
        {
            case ScreenType.DisplayText:
            case ScreenType.EmailMenu:
            case ScreenType.Email:
            case ScreenType.Password:
            case ScreenType.PasswordFail:
            case ScreenType.None:
            default:
                canvasGroup.alpha = 0;
                break;
            case ScreenType.Menu:
                canvasGroup.alpha = 1;
                break;

        }
    }
}
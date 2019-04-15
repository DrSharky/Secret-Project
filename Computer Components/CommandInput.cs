using UnityEngine;
using System.Linq;

public class CommandInput : MonoBehaviour
{
    [SerializeField]
    private EmailCommandList emailInfo;

    #region Header Variables
    [SerializeField]
    private GameObject headerObject;

    private RectTransform headerRectTransform;
    private UnityEngine.UI.Text headerText;
    #endregion

    #region InputField Variables
    [SerializeField]
    private GameObject inputField;
    private RectTransform inputFieldRectTransform;
    private TMPro.TMP_InputField tmInputField;
    #endregion

    #region Caret Variables
    [SerializeField]
    private GameObject caretObject;
    private TMPro.TMP_Text caretText;
    private Caret caretScript;
    #endregion

    #region Command Input Configurations
    [SerializeField]
    private CommandConfig commandConfig;
    [SerializeField]
    private CommandConfig passConfig;
    [SerializeField]
    private CommandConfig displayConfig;
    [SerializeField]
    private CommandConfig emailConfig;
    #endregion

    private CanvasGroup commandCanvas;

    void Awake()
    {
        commandCanvas = GetComponent<CanvasGroup>();

        headerRectTransform = headerObject.GetComponent<RectTransform>();
        headerText = headerObject.GetComponent<UnityEngine.UI.Text>();

        inputFieldRectTransform = inputField.GetComponent<RectTransform>();
        tmInputField = inputField.GetComponent<TMPro.TMP_InputField>();

        caretText = caretObject.GetComponent<TMPro.TMP_Text>();
        caretScript = caretObject.GetComponent<Caret>();

        commandConfig.headerString = CommonCompStrings.inputDict[CommonCompStrings.Input.CmdHeader];
        passConfig.headerString = CommonCompStrings.inputDict[CommonCompStrings.Input.PassHeader];
        displayConfig.headerString = CommonCompStrings.inputDict[CommonCompStrings.Input.Continue];
    }

    void CommandSetup(CommandConfig config)
    {
        //Header Text
        headerRectTransform.anchorMin = config.headerAnchor;
        headerRectTransform.anchorMax = config.headerAnchor;
        //Set position and text.
        headerRectTransform.anchoredPosition = config.headerPos;
        headerText.text = config.headerString;

        headerText.rectTransform.sizeDelta = config.headerVector;

        //InputField
        //Set position and reset text.
        inputFieldRectTransform.anchoredPosition = config.inputPos;
        tmInputField.text = CommonCompStrings.charDict[CommonCompStrings.Char.Empty];

        caretScript.originPos = config.caretPos;
    }

    public void SwitchState(ScreenType state)
    {
        if (commandCanvas.alpha == 0)
            commandCanvas.alpha = 1;

        if (!caretObject.activeInHierarchy)
            caretObject.SetActive(true);

        switch (state)
        {
            //case 1 for normal command input.
            case ScreenType.Menu:
                CommandSetup(commandConfig);
                SelectCommandText();
                break;
            //case 2 for password input.
            case ScreenType.Password:
                CommandSetup(passConfig);
                SelectCommandText();
                break;
            //case 3 for display text setup.
            case ScreenType.DisplayText:
            case ScreenType.PasswordSucceed:
            case ScreenType.Error:
            case ScreenType.Help:
                caretObject.SetActive(false);
                CommandSetup(displayConfig);
                break;
            //case 4 for email text.
            case ScreenType.Email:
            case ScreenType.EmailMenu:
                GetEmailCommandHeader();
                CommandSetup(emailConfig);
                SelectCommandText();
                break;
            //Leave case for password fail blank, it shouldn't change anything.
            case ScreenType.PasswordFail:
                break;
            //default for hiding the display.
            case ScreenType.None:
            default:
                commandCanvas.alpha = 0;
                break;
        }
        ResetCommandText();
    }

    void ToggleCanvas()
    {
        if (commandCanvas.alpha == 1)
            commandCanvas.alpha = 0;
        else
            commandCanvas.alpha = 1;
    }

    void ResetCommandText()
    {
        tmInputField.interactable = true;
        tmInputField.text = null;
        caretScript.Reset();
    }

    public void SelectCommandText()
    {
        tmInputField.Select();
        tmInputField.ActivateInputField();
    }

    //TODO: Update this when returning to email Menu
    void GetEmailCommandHeader()
    {
        int emailCount = emailInfo.Commands.Where(x => x.showEmail).Count();
        emailConfig.headerString = emailCount + " total emails, choose 1..." + emailCount + "\n[n]ext or [p]rev for more emails\n[q]uit to exit: ";
    }
}
using UnityEngine;

public class CommandInput : MonoBehaviour
{
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

    [SerializeField]
    private GameObject caretObject;
    private TMPro.TMP_Text caretText;
    private Caret caretScript;

    [SerializeField]
    private CommandConfig commandConfig;
    [SerializeField]
    private CommandConfig passConfig;

    private CanvasGroup commandCanvas;

    private System.Action<ScreenType> activateListener;
    private System.Action toggleListener;

    void Awake()
    {
        commandCanvas = GetComponent<CanvasGroup>();

        headerRectTransform = headerObject.GetComponent<RectTransform>();
        headerText = headerObject.GetComponent<UnityEngine.UI.Text>();

        inputFieldRectTransform = inputField.GetComponent<RectTransform>();
        tmInputField = inputField.GetComponent<TMPro.TMP_InputField>();

        caretText = caretObject.GetComponent<TMPro.TMP_Text>();
        caretScript = caretObject.GetComponent<Caret>();

        activateListener = new System.Action<ScreenType>((state) => SwitchState(state));
        EventManager.StartListening("State" + commandCanvas.name, activateListener);

        toggleListener = new System.Action(() => ToggleCanvas());
        EventManager.StartListening("Toggle" + commandCanvas.name, toggleListener);

        commandConfig.headerString = StringManager.commandHeaderText;
        passConfig.headerString = StringManager.passHeaderText;
    }

    void CommandSetup(CommandConfig config)
    {
        //Header Text
        headerRectTransform.anchorMin = config.headerAnchor;
        headerRectTransform.anchorMax = config.headerAnchor;
        //Set position and text.
        headerRectTransform.anchoredPosition = config.headerPos;
        headerText.text = config.headerString;

        //InputField
        //Set position and reset text.
        inputFieldRectTransform.anchoredPosition = config.inputPos;
        tmInputField.text = StringManager.emptyString;

        caretScript.originPos = config.caretPos;
    }

    //TODO: Write logic for remaining possible state changes.
    void SwitchState(ScreenType state)
    {
        if (commandCanvas.alpha == 0)
            commandCanvas.alpha = 1;

        switch (state)
        {
            //case 1 for normal command input.
            case ScreenType.Normal:
                CommandSetup(commandConfig);
                break;
            //case 2 for password input.
            case ScreenType.Password:
                CommandSetup(passConfig);
                break;
            //case 3 for display text setup.
            case ScreenType.DisplayText:
                break;
            //case 4 for email text.
            case ScreenType.Email:
            case ScreenType.EmailMenu:
                break;
            //Leave case for password fail blank, it shouldn't change anything.
            case ScreenType.PasswordFail:
                break;
            //default for hiding the display.
            default:
                ToggleCanvas();
                break;
        }
    }

    void ToggleCanvas()
    {
        if (commandCanvas.alpha == 1)
            commandCanvas.alpha = 0;
        else
            commandCanvas.alpha = 1;
        //commandCanvas.enabled = !commandCanvas.enabled;
    }
}
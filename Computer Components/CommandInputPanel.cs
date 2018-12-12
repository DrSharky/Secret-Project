public class CommandInputPanel : MenuPanel, IDisplayPanel, IPasswordInputPanel
{
    public System.Action<bool> DisplayPanelListener { get; set; }
    public System.Action<bool> PasswordInputPanelListener { get; set; }

    private TMPro.TMP_InputField commandInputText;
    private string displayText = "[Press \"ENTER\" to continue]";
    private string emptyStr = "";

    public override void Start()
    {
        base.Start();

        commandInputText = GetComponentInChildren<TMPro.TMP_InputField>();
        DisplayPanelListener = new System.Action<bool>((activate) =>
        {
            if (activate)
            {
                Children[0].color = Inactive;
                commandInputText.text = displayText;
            }
            else
            {
                Children[0].color = Active;
                commandInputText.text = emptyStr;
            }
        });
        EventManager.StartListening(EventManager.displayPanelToggle + transform.parent.name, DisplayPanelListener);
        GenerateActivationEvent(PasswordInputPanelListener, EventManager.passwordPanelToggle, false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        StopEventListening(EventManager.displayPanelToggle, DisplayPanelListener);
        StopEventListening(EventManager.passwordPanelToggle, PasswordInputPanelListener);
    }
}
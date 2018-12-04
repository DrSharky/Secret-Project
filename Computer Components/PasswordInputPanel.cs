public class PasswordInputPanel : ComputerPanel, IPasswordInputPanel
{
    public System.Action<bool> PasswordInputPanelListener { get; set; }

    public override void Start()
    {
        base.Start();
        GenerateActivationEvent(PasswordInputPanelListener, EventManager.passwordPanelToggle, true);
    }
}
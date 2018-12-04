public class EmailPanel : ComputerPanel, IEmailPanel
{
    public System.Action<bool> EmailPanelListener { get; set; }

    public override void Start()
    {
        base.Start();
        GenerateActivationEvent(EmailPanelListener, EventManager.emailPanelToggle, true);
    }
}
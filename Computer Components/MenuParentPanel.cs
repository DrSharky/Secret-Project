//Have to repeat displayPanelListener anyways, because menuParentlPanel is opposite operations
//from the displayPanel. Repeat code is almost the same, not exactly. (So don't feel bad.)
public class MenuParentPanel : MenuPanel, IDisplayPanel, IEmailPanel
{
    public System.Action<bool> DisplayPanelListener { get; set; }
    public System.Action<bool> EmailPanelListener { get; set; }

    public override void Start()
    {
        base.Start();
        //Set the activate bool to !activate, to do the opposite operation to the displayPanel.
        GenerateActivationEvent(DisplayPanelListener, EventManager.displayPanelToggle, false);

        //Set the activate bool to !activate, to do the opposite operation to the emailPanel.
        GenerateActivationEvent(EmailPanelListener, EventManager.emailPanelToggle, false);
    }
}
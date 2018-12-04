public class DisplayPanel : ComputerPanel, IDisplayPanel
{
    public System.Action<bool> DisplayPanelListener { get; set; }

    public override void Start()
    {
        base.Start();
        GenerateActivationEvent(DisplayPanelListener, EventManager.displayPanelToggle, true);
    }
}
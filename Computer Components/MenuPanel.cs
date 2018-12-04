public class MenuPanel : ComputerPanel, IMenuPanel
{
    public System.Action<bool> MenuPanelListener { get; set; }

    public override void Start()
    {
        base.Start();
        MenuPanelListener = new System.Action<bool>((activate) =>
        { if (activate) { ActivatePanel(); } else { DeactivatePanel(); } });
        EventManager.StartListening(EventManager.menuPanelToggle + transform.parent.name, MenuPanelListener);
    }
}
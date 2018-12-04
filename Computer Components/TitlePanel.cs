﻿public class TitlePanel: ComputerPanel, ITitlePanel
{
    public System.Action<bool> TitlePanelListener { get; set; }

    public override void Start()
    {
        base.Start();
        GenerateActivationEvent(TitlePanelListener, EventManager.titlePanelToggle, true);
    }
}
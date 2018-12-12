using UnityEngine;

public interface IComputerPanel
{
    Color Inactive { get; set; }
    Color Active { get; set; }
    UnityEngine.UI.Text[] Children { get; set; }
    void Start();
    void ActivatePanel();
    void DeactivatePanel();
    System.Action ExitScreenListener { get; set; }
    void OnDestroy();
}

public interface IMenuPanel : IComputerPanel
{
    System.Action<bool> MenuPanelListener { get; set; }
}

public interface IDisplayPanel : IComputerPanel
{
    System.Action<bool> DisplayPanelListener { get; set; }
}

public interface IEmailPanel : IComputerPanel
{
    System.Action<bool> EmailPanelListener { get; set; }
}

public interface IPasswordInputPanel : IComputerPanel
{
    System.Action<bool> PasswordInputPanelListener { get; set; }
}

public interface ITitlePanel : IComputerPanel
{
    System.Action<bool> TitlePanelListener { get; set; }
}
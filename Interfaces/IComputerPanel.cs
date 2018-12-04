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
}

public interface IMenuPanel
{
    System.Action<bool> MenuPanelListener { get; set; }
}

public interface IDisplayPanel
{
    System.Action<bool> DisplayPanelListener { get; set; }
}

public interface IEmailPanel
{
    System.Action<bool> EmailPanelListener { get; set; }
}

public interface IPasswordInputPanel
{
    System.Action<bool> PasswordInputPanelListener { get; set; }
}

public interface ITitlePanel
{
    System.Action<bool> TitlePanelListener { get; set; }
}
using UnityEngine;

public class ComputerPanel : MonoBehaviour, IComputerPanel
{
    private Color invisible = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public Color Inactive { get { return invisible; } set { invisible = value; } }
    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color Active { get { return visible; } set { visible = value; } }
    public UnityEngine.UI.Text[] Children { get; set; }
    public System.Action<bool> ExitScreenListener { get; set; }

    public virtual void Start()
    {
        Children = GetComponentsInChildren<UnityEngine.UI.Text>();
        ExitScreenListener = new System.Action<bool>((activate) => { DeactivatePanel(); });
        EventManager.StartListening(EventManager.exitScreenEvent + transform.parent.name, ExitScreenListener);

        //Can only exit screen when outside of email menus. So the exit event can be used to
        //disable the other panels when entering the email menu.
        EventManager.StartListening(EventManager.emailPanelToggle + transform.parent.name, ExitScreenListener);
    }

    public virtual void ActivatePanel()
    {
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].color = Active;
        }
    }

    public virtual void DeactivatePanel()
    {
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].color = Inactive;
        }
    }

    public virtual void GenerateActivationEvent(System.Action<bool> action, string eventName, bool activation)
    {
        if (activation)
        {
            action = new System.Action<bool>((activate) =>
            { if (activate) { ActivatePanel(); } else { DeactivatePanel(); } });
        }
        else
        {
            action = new System.Action<bool>((activate) =>
            { if (!activate) { ActivatePanel(); } else { DeactivatePanel(); } });
        }

        EventManager.StartListening(eventName + transform.parent.name, action);
    }

    public virtual void StopEventListening(string eventName, System.Action action)
    {
        EventManager.StopListening(eventName + transform.parent.name, action);
    }

    public virtual void StopEventListening<T>(string eventName, System.Action<T> action)
    {
        EventManager.StopListening(eventName + transform.parent.name, action);
    }

    public virtual void OnDestroy()
    {
        StopEventListening(EventManager.exitScreenEvent, ExitScreenListener);
    }
}
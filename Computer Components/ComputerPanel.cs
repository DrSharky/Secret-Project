using UnityEngine;

public class ComputerPanel : MonoBehaviour, IComputerPanel
{
    private Color invisible = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public Color Inactive { get { return invisible; } set { invisible = value; } }
    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color Active { get { return visible; } set { visible = value; } }
    public UnityEngine.UI.Text[] Children { get; set; }
    public System.Action ExitScreenListener { get; set; }

    public virtual void Start()
    {
        Children = GetComponentsInChildren<UnityEngine.UI.Text>();
        ExitScreenListener = new System.Action(() => { DeactivatePanel(); });
        EventManager.StartListening(EventManager.exitScreenEvent + transform.parent.name, ExitScreenListener);
    }

    public void ActivatePanel()
    {
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].color = Active;
        }
    }

    public void DeactivatePanel()
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
}
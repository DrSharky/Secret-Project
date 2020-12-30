using UnityEngine;

public abstract class EventInteractable : Interactable
{
    const string activate = "Activate ";

    public System.Action activateListener { get; set; }
    public virtual void Awake()
    {
        activateListener = new System.Action(() => Activate());
        EventManager.StartListening(activate + gameObject.name, activateListener);
    }
}
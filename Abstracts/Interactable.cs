using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The boolean to check whether the interactable
    /// should be highlighted or not.
    /// </summary>
    public bool highlight = true;

    /// <summary>
    /// The boolean to check whether or not using the interactable
    /// freezes the player.
    /// </summary>
    public bool freezePlayer = false;

    public virtual void Activate() { }

    public System.Action activateListener { get; set; }
    public virtual void Awake()
    {
        activateListener = new System.Action(() => Activate());
        EventManager.StartListening("Activate" + gameObject.name, activateListener);
    }
}
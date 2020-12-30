using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Fields (Inherited)")]
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

    public abstract void Activate();
}
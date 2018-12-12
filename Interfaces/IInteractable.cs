using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any object that the player can interact with 
/// by pressing the use key.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// The method to run when the player uses the interactable.
    /// </summary>
    void Activate();

    /// <summary>
    /// The boolean to check whether or not using the interactable
    /// freezes the player.
    /// </summary>
    bool freezePlayer { get; set; }

    /// <summary>
    /// The boolean to check whether the interactable
    /// should be highlighted or not.
    /// </summary>
    bool highlight { get; set; }

}
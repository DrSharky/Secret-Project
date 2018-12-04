using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any object that the player can interact with 
/// by pressing the use key.
/// </summary>
public interface IInteractable
{
    void Activate();
    bool freezePlayer { get; set; }
}
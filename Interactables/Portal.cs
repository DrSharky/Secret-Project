using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Like a door, but changes maps.
/// </summary>
public class Portal : IInteractable
{

    private bool freeze = false;
    public bool freezePlayer { get { return freeze; } set { freeze = value; } }
    public string sceneToLoad;

    public void Activate()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
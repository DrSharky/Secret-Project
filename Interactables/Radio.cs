using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    private bool freeze = false;
    public bool freezePlayer { get { return freeze; } set { freeze = value; } }
    private bool highlightable = true;
    public bool highlight { get { return highlightable; } set { highlightable = value; } }
    public AudioSource radioShow;
    private bool initialized = false;

    public void Activate()
    {
        if (!initialized)
        {
            radioShow.Play();
            initialized = true;
        }
        else if (radioShow.volume == 0.26f)
            radioShow.volume = 0.0f;
        else
            radioShow.volume = 0.26f;
    }
}

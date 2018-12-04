using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    private bool freeze = false;
    public bool freezePlayer { get { return freeze; } set { freeze = value; } }
    public AudioSource DebOfNight;
    private bool initialized = false;

    public void Activate()
    {
        if (!initialized)
        {
            DebOfNight.Play();
            initialized = true;
        }
        else if (DebOfNight.volume == 0.26f)
            DebOfNight.volume = 0.0f;
        else
            DebOfNight.volume = 0.26f;
    }
}

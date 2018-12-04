using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Edit Container class to check for when the player stops using the container.
//This is why the MonoBehaviour class is included in inheritance here.

/// <summary>
/// Class to define any object that acts as storage for items.
/// </summary>
public class Container : MonoBehaviour, IInteractable
{
    private bool freeze = true;

    public bool freezePlayer { get { return freeze; } set { freeze = value; } }

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip openClip;
    [SerializeField]
    private AudioClip closeClip;

    public void Activate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            anim.SetTrigger("close");
            if (closeClip != null)
            {
                soundSource.clip = closeClip;
                soundSource.Play();
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("open");
            soundSource.clip = openClip;
            soundSource.Play();
        }
        else
            return;
    }
}

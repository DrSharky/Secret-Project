using UnityEngine;

/// <summary>
/// Class to define any object that acts as storage for items.
/// </summary>
public class Container : Interactable
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip openClip;
    [SerializeField]
    private AudioClip closeClip;

    void Awake()
    {
        freezePlayer = true;
    }

    public override void Activate()
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

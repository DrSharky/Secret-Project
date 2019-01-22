using UnityEngine;

public class Radio : Interactable
{
    public AudioSource radioShow;
    private bool initialized = false;

    public override void Activate()
    {
        {
            if (!initialized)
            {
                radioShow.Play();
                initialized = true;
            }
            radioShow.volume = 0.26f;
        }
        else
            radioShow.volume = 0.0f;
    }
}

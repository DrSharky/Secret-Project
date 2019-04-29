using UnityEngine;

public class Radio : Interactable
{
    public AudioSource radioShow;
    public bool startOn = true;
    private bool initialized = false;

    void Start()
    {
        if(startOn)
        {
            radioShow.Play();
            initialized = true;
            radioShow.volume = 0.12f;
        }
    }

    public override void Activate()
    {
        if (radioShow.volume == 0.0f)
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

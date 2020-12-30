using UnityEngine;

public class Radio : Interactable
{
    public AudioSource radioShow;
    public bool startOn = true;
    private bool initialized = false;
    private float startVol;

    private void Awake()
    {
        startVol = radioShow.volume;
        if (!startOn)
            radioShow.volume = 0.0f;
    }

    void Start()
    {
        if(startOn)
        {
            radioShow.Play();
            initialized = true;
            radioShow.volume = startVol;
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
            radioShow.volume = startVol;
        }
        else
            radioShow.volume = 0.0f;
    }
}

using UnityEngine;

public class AudioPause : MonoBehaviour
{
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void TogglePause(bool pause)
    {
        if (pause)
            source.Pause();
        else
            source.UnPause();
    }
}

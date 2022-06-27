using UnityEngine;
using UnityEngine.Audio;

public class AudioPause : MonoBehaviour
{
    public AudioMixer audioMix;
    bool toggle;

    public void TogglePause()
    {
        if (!toggle)
        {
            audioMix.SetFloat("Default", -80.0f);
            audioMix.SetFloat("Inventory", 0f);
        }
        else
        {
            audioMix.SetFloat("Inventory", -80.0f);
            audioMix.SetFloat("Default", 0f);
        }
        toggle = !toggle;
    }
}
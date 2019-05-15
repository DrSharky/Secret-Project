using System.Collections;
using UnityEngine;

public class SoundScape : MonoBehaviour
{
    [SerializeField]
    private AudioSource ambientMain;

    // Initialize in Inspector
    [SerializeField]
    private RandomAudioClip[] soundClips;
    // Initialize in Inspector
    [SerializeField]
    private GameObject randomAudioSource;

    private bool playSound = true;
	
	// Update is called once per frame
	void Update ()
    {
        if(playSound)
            StartCoroutine(PlayRandomSound());
    }

    IEnumerator TimeRandom()
    {
        yield return new WaitForSeconds(Random.Range(5.7f, 17.8f));
        playSound = true;
    }

    IEnumerator PlayRandomSound()
    {
        playSound = false;
        StartCoroutine(TimeRandom());
        int clipIndex = Random.Range(0, soundClips.Length);
        RandomAudioClip selectedClip = soundClips[clipIndex];
        float distanceFromPlayer = Random.Range(selectedClip.distMin, selectedClip.distMax);
        float heightFromSource = Random.Range(selectedClip.heightMin, selectedClip.heightMax);
        // --Polar coordinates--
        // The axes
        // pos x = +z
        // pos y = -x
        float angleFromSource = Random.Range(selectedClip.angleMin, selectedClip.angleMax);
        Vector3 newDir = PolarToCartesian(angleFromSource, distanceFromPlayer, heightFromSource);
        GameObject newAudio = Instantiate(randomAudioSource, newDir, transform.rotation);
        AudioSource randomSource = newAudio.GetComponent<AudioSource>();
        randomSource.clip = selectedClip.audioClip;
        randomSource.volume = selectedClip.volume;
        randomSource.pitch = Random.Range(selectedClip.pitchMin, selectedClip.pitchMax);
        randomSource.maxDistance = selectedClip.radius;
        randomSource.Play();
        yield return new WaitForSeconds(selectedClip.audioClip.length);
        Destroy(newAudio);
    }

    Vector3 PolarToCartesian(float angle, float distance, float height)
    {
        float xDir = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        float zDir = -distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        return new Vector3(transform.position.x - (xDir/2), height, transform.position.z - (zDir/2));
    }

    public void TogglePause(bool pause)
    {
        if (pause)
            playSound = false;
    }
}
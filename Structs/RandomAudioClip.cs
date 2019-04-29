using UnityEngine;

[System.Serializable]
public struct RandomAudioClip
{
    [SerializeField]
    private string description;


    // The audio clip to play
    public AudioClip audioClip;    
    // Pitch range
    [Tooltip("0.0-1.0")]
    public float pitchMin, pitchMax;
    // Volume
    [Tooltip("0.0-1.0")]
    public float volume;
    // Random chance modifier
    [Tooltip("Random chance modifier, does nothing yet.")]
    public float frequency;
    // Audible distance radius
    public float radius;
    // Distance range baseed on player position
    public float distMin, distMax;
    // Height range based on sound scape game object position
    [Tooltip("Height range based on soundscape gameObject origin.")]
    public float heightMin, heightMax;
    // Angle range
    [Tooltip("Angle range based on soundscape gameObject origin.")]
    public float angleMin, angleMax;
}
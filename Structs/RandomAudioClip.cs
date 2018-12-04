using UnityEngine;

[System.Serializable]
public struct RandomAudioClip
{
    [SerializeField]
    private string description;

    // The audio clip to play
    public AudioClip audioClip;    
    // Pitch range
    public float pitchMin, pitchMax;
    // Volume
    public float volume;
    // Random chance modifier
    public float frequency;
    // Audible distance radius
    public float radius;
    // Distance range baseed on player position
    public float distMin, distMax;
    // Height range based on sound scape game object position
    public float heightMin, heightMax;
    // Angle range
    public float angleMin, angleMax;
}
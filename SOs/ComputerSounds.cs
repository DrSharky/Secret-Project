using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Computer Sounds")]
public class ComputerSounds : Sirenix.OdinInspector.SerializedScriptableObject
{
    public enum Clips
    {
        Accept,
        Access,
        Error,
        Typing
    }

    public Dictionary<Clips, AudioClip> audioDict = new Dictionary<Clips, AudioClip>();
}
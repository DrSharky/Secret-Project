using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ComputerSounds : SerializedScriptableObject
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComputerSounds
{
    public enum Clips
    {
        Accept,
        Access,
        Error,
        Typing
    }

    public static Dictionary<Clips, AudioClip> audioDict = new Dictionary<Clips, AudioClip>();
}
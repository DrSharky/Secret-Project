using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using RPG;

[CreateAssetMenu(menuName ="Character")]
public class CharacterObject : SerializedScriptableObject
{
    public string charName;
    public List<Stat> stats;
    public int xp;
    public Clan clan;
}

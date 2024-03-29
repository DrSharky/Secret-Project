﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName ="Clan Definition")]
public class ClanDefinition : SerializedScriptableObject
{
    public Clan clan;
    public List<Discipline> disciplines;
    public StatModDictionary statModifiers;
}

[System.Serializable]
public class StatModDictionary : SerializedDictionary<StatId, int> { }
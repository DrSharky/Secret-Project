using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create InventoryDB")]
public class InventoryDatabase : ScriptableObject
{
    public InventoryDictionary database;
}

[System.Serializable]
public class InventoryDictionary : SerializedDictionary<InventoryCategory, InventoryObject> { }
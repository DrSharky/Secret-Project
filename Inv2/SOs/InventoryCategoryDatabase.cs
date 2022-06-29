using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryCategoryDB")]
public class InventoryCategoryDatabase : ScriptableObject
{
    public InventoryCategoryDictionary database;
}

[System.Serializable]
public class InventoryCategoryDictionary : SerializedDictionary<InventoryCategory, Sprite> { }

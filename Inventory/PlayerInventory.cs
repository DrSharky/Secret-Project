using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    [SerializeField]
    public List<InventoryObject> inventory;
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    [SerializeField]
    public List<InventoryObject> generalItems;
    [SerializeField]
    public List<InventoryObject> questItems;
    [SerializeField]
    public List<InventoryObject> firearmItems;
    [SerializeField]
    public List<InventoryObject> meleeItems;
    [SerializeField]
    public List<InventoryObject> clothingItems;
}

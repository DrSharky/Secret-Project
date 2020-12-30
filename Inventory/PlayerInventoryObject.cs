using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryObject
{
    //The actual object in the inventory.
    public InventoryObject inventoryObject;
    //The amount of the objects that the player has.
    public int amountHeld;

    public PlayerInventoryObject(InventoryObject invObj = null, int amt = 0)
    {
        inventoryObject = invObj;
        amountHeld = amt;
    }
}
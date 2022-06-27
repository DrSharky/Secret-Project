using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Slots = new List<InventorySlot>();

    public void Clear()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].item = new Item();
            Slots[i].amount = 0;
        }
    }

    public bool ContainsItem(ItemObject itemObject)
    {
        return Slots.Where(i => i.item.Id == itemObject.data.Id) != null;
        //return Array.Find<InventorySlot>(Slots, i => i.item.Id == itemObject.data.Id) != null;
    }


    public bool ContainsItem(int id)
    {
        return Slots.FirstOrDefault(i => i.item.Id == id) != null;
    }
}
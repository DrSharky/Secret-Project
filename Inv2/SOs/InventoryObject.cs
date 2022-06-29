using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public InterfaceType type;
    public InventoryCategory category;
    //public int MAX_ITEMS;
    [SerializeField]
    private Inventory Container = new Inventory();
    public List<InventorySlot> GetSlots => Container.Slots;

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Count; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    public int ItemCount
    {
        get
        {
            int counter = 0;
            for(int i = 0; i < GetSlots.Count; i++)
            {
                if(GetSlots[i].item.Id > -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    public bool AddItem(Item item, int amount)
    {
        if (EmptySlotCount <= 0)
        {
            return false;
        }
        InventorySlot slot = FindItemOnInventory(item);
        if (slot == null)
        {
            GetEmptySlot().UpdateSlot(item, amount);
            return true;
        }
        if (database.ItemObjects[item.Id].stackable)
        {
            slot.AddAmount(amount);
            return true;
        }

        return false;
    }

    public InventorySlot FindItemOnInventory(Item item)
    {
        for (int i = 0; i < GetSlots.Count; i++)
        {
            if (GetSlots[i].item.Id == item.Id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }


    public bool IsItemInInventory(ItemObject item)
    {
        for (int i = 0; i < GetSlots.Count; i++)
        {
            if (GetSlots[i].item.Id == item.data.Id)
            {
                return true;
            }
        }
        return false;
    }


    public InventorySlot GetEmptySlot()
    {
        for (int i = 0; i < GetSlots.Count; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                return GetSlots[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item1 == item2)
            return;
        if (item2.CanPlaceInSlot(item1.GetItemObject()) && item1.CanPlaceInSlot(item2.GetItemObject()))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }

    }


    [ContextMenu("Save")]
    public void Save()
    {
        #region Optional Save
        //string saveData = JsonUtility.ToJson(Container, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();
        #endregion

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            #region Optional Load
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), Container);
            //file.Close();
            #endregion

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            int loadedInvCount = newContainer.Slots.Count(x => x.item.Id > -1);
            for(int i = 0; i < loadedInvCount; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            //for (int i = 0; i < GetSlots.Length; i++)
            //{
            //    GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            //}
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }

}


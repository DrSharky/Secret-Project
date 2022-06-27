using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;

#if !UNITY_EDITOR
    private void Awake()
    {
        inventory.Load();
    }
#endif


    public void OnTriggerEnter(Collider other)
    {

        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            item.particles.Play();
            //inventory.AddItem(item.item.CreateItem(), 1);
            //if (inventory.AddItem(new Item(item.item), 1))
            //{
            //    Destroy(other.gameObject);
            //}
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            item.particles.Stop();
        }
    }

    //private void LateUpdate()
    //{
    //    if (Input.GetKeyUp(KeyCode.KeypadPeriod))
    //    {
    //        inventory.Save();
    //        equipment.Save();
    //    }
    //    else if (Input.GetKeyUp(KeyCode.KeypadEnter))
    //    {
    //        inventory.Load();
    //        equipment.Load();
    //    }
    //}

    //public void OnApplicationQuit()
    //{
    //    inventory.Clear();
    //    equipment.Clear();
    //}
}
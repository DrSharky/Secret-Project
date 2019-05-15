using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public struct ItemUIObject
{
    public GameObject itemFrame;
    public GameObject selectFrame;
    public GameObject itemImage;
}

public class InventoryManager : MonoBehaviour
{
    public GameEvent inventoryToggle;
    public PlayerInventory inventory;
    public RectTransform headImageTransform;

    bool inventoryActive = false;
    List<ItemType> types = new List<ItemType>{ ItemType.General, ItemType.Melee, ItemType.Firearm, ItemType.Clothing, ItemType.Quest };
    List<ItemUIObject> generalItems, fireArms, meleeWeps, questItems, clothing;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryActive)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
            inventoryActive = !inventoryActive;
            inventoryToggle.Raise();
            inventoryToggle.sentBool = !inventoryToggle.sentBool;
        }
    }

    void Awake()
    {
        if (!inventoryToggle.sentBool)
            inventoryToggle.sentBool = true;
    }

    void Start()
    {
        foreach(ItemType type in types)
        {
            CreateInventoryItemPanels(type);
        }
    }

    void CreateInventoryItemPanels(ItemType type)
    {
        //Using the headImageTransform as a reference point,
        //instantiate new UI elements under it.
        //I guess this depends on the player's inventory?

        List<InventoryObject> objects = inventory.items.FindAll(x => x.type == type).ToList();

        for(int i = 0; i < objects.Count; i++)
        {
            
        }



    }
}

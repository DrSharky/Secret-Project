using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemFrame;
    public GameObject itemFrameActive;
    public GameObject selectFrame;
    public GameObject itemImage;

    public GameEvent inventoryToggle;
    public PlayerInventory inventory;
    public RectTransform headImageTransform;

    Vector2 anchorVector = new Vector2(0f, 1f);
    bool inventoryActive = false;
    List<ItemType> types = new List<ItemType>{ ItemType.General, ItemType.Melee, ItemType.Firearm, ItemType.Clothing, ItemType.Quest };

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !Computer.usingComputer)
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

    //TODO: WIP... Still need to add logic for multiple columns if there are enough items.
    void CreateInventoryItemPanels(ItemType type)
    {
        //Using the headImageTransform as a reference point,
        //instantiate new UI elements under it.
        //I guess this depends on the player's inventory?

        List<InventoryObject> objects = inventory.generalItems.FindAll(x => x.type == type).ToList();

        GameObject itemGroupObj = new GameObject();
        itemGroupObj.AddComponent<CanvasGroup>();
        CanvasGroup group = itemGroupObj.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
        itemGroupObj.transform.parent = transform;
        itemGroupObj.transform.position = headImageTransform.position;

        for (int i = 0; i < objects.Count; i++)
        {
            //--Item Frame--
            GameObject itemFrameCopy = Instantiate(itemFrame, itemGroupObj.transform.position, 
                                itemGroupObj.transform.rotation, itemGroupObj.transform);
            RectTransform itemFrameTransform = itemFrameCopy.GetComponent<RectTransform>();
            itemFrameTransform.anchorMin = anchorVector;
            itemFrameTransform.anchorMax = anchorVector;
            itemFrameTransform.anchoredPosition += new Vector2(18, -itemFrameTransform.rect.height * (i + 1.5f));
            //--Item Frame--

            //--Item Frame Active--
            GameObject itemFrameActiveCopy = Instantiate(itemFrameActive, itemGroupObj.transform.position,
                                itemGroupObj.transform.rotation, itemGroupObj.transform);
            RectTransform itemFrameActiveTransform = itemFrameActiveCopy.GetComponent<RectTransform>();
            itemFrameActiveTransform.anchorMin = anchorVector;
            itemFrameActiveTransform.anchorMax = anchorVector;
            itemFrameActiveTransform.anchoredPosition += new Vector2(18, -itemFrameActiveTransform.rect.height * (i + 1.5f));
            //--Item Frame Active--

            //--Item Image--
            GameObject itemImageCopy = Instantiate(itemImage, itemGroupObj.transform.position,
                                itemGroupObj.transform.rotation, itemGroupObj.transform);
            RectTransform itemImageTransform = itemImageCopy.GetComponent<RectTransform>();
            RawImage rawItem = itemImageCopy.GetComponent<RawImage>();
            rawItem.texture = objects[i].inventoryImage;
            itemImageTransform.anchorMin = anchorVector;
            itemImageTransform.anchorMax = anchorVector;
            itemImageTransform.anchoredPosition += new Vector2(15, (-itemImageTransform.rect.height * (i + 1.5f)) - 0.5f);
            //--Item Image--

            //--Select Frame--
            GameObject selectFrameCopy = Instantiate(selectFrame, itemGroupObj.transform.position,
                                itemGroupObj.transform.rotation, itemGroupObj.transform);
            RectTransform selectFrameTransform = selectFrameCopy.GetComponent<RectTransform>();
            selectFrameTransform.anchorMin = anchorVector;
            selectFrameTransform.anchorMax = anchorVector;
            selectFrameTransform.anchoredPosition += new Vector2(22, (-itemFrameTransform.rect.height * (i + 1.5f))-5);
            //--Select Frame--

        }
    }

    //TODO: Add methods for adding and removing items from inventory & updating UI when this happens.

    public void RemoveInventoryItem()
    {

    }

    public void AddInventoryItem()
    {

    }
}

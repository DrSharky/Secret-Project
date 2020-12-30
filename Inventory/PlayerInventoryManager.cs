using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DataPersistence;

public class PlayerInventoryManager : MonoBehaviour
{

    public GameObject itemFrame;
    public GameObject itemFrameActive;
    public GameObject selectFrame;
    public GameObject itemImage;
    public GameObject inventoryItemGroup;
    public GameObject itemParent;
    public RectTransform headImageTransform;
    //public PlayerInventory inventory;

    public GameEvent inventoryOpen;
    public GameEvent inventoryClose;

    [Header("Addition Game Events")]
    public GameEvent addGeneralItem;
    public GameEvent addMeleeItem;
    public GameEvent addFirearmItem;
    public GameEvent addClothingItem;
    public GameEvent addQuestItem;
    
    IDataPersistence localPersistence;

    //Vector2 anchorVector = new Vector2(0f, 1f);
    bool inventoryActive = false;

    void Update()
    {

        //TODO: Change this variable from usingComputer to something that is generic
        //that will change if player cannot open inventory currently.
        //if (Input.GetKeyDown(KeyCode.I) && !Computer.usingComputer)
        //{
        //    if (!inventoryActive)
        //    {
        //        Time.timeScale = 0f;
        //        inventoryOpen.Raise();
        //    }
        //    else
        //    {
        //        Time.timeScale = 1f;
        //        inventoryClose.Raise();
        //    }
        //    inventoryActive = !inventoryActive;
        //    //inventoryToggle.Raise();
        //    //inventoryToggle.sentBool = !inventoryToggle.sentBool;
        //}
    }

    public void AddToInventory(InventoryObjectScript invObjScript)
    {

    }

    public InventoryDefinition kilpat;

    void Awake()
    {
        JsonDataSerializer dataSerializer = new JsonDataSerializer();
        localPersistence = new LocalPersistence(dataSerializer);


        GameFoundation.Initialize(localPersistence);
        InventoryManager.CreateInventory(kilpat, kilpat.id);

        //if (!inventoryToggle.sentBool)
        //    inventoryToggle.sentBool = true;
    }

    public void Save()
    {
        GameFoundation.Save(localPersistence);
    }

    public void Load()
    {
        GameFoundation.Load(localPersistence);
    }

    //void Start()
    //{
    //    foreach (ItemType type in System.Enum.GetValues(typeof(ItemType)))
    //    {
    //        CreateInventoryItemPanels(type);
    //    }
    //}

    //TODO: WIP... Still need to add logic for multiple columns if there are enough items.
    public void CreateInventoryItemPanels(ItemType type)
    {
        //Using the headImageTransform as a reference point,
        //instantiate new UI elements under it.
        //I guess this depends on the player's inventory?

        //List<InventoryObject> objects = inventory.generalItems.FindAll(x => x.type == type).ToList();
        List<PlayerInventoryObject> objects;

        GameObject itemGroupObj = Instantiate(inventoryItemGroup, transform);
        itemGroupObj.GetComponent<CanvasGroup>().alpha = 0;

        //switch (type)
        //{
        //    case ItemType.Clothing:
        //        objects = inventory.clothingItems;
        //        break;
        //    case ItemType.Firearm:
        //        objects = inventory.firearmItems;
        //        break;
        //    case ItemType.General:
        //        objects = inventory.generalItems;
        //        break;
        //    case ItemType.Melee:
        //        objects = inventory.meleeItems;
        //        break;
        //    case ItemType.Quest:
        //        objects = inventory.questItems;
        //        break;
        //    default:
        //        objects = null;
        //        break;
        //}

        //itemGroupObj.AddComponent<CanvasGroup>();
        //InventoryUIToggle toggle = itemGroupObj.AddComponent<InventoryUIToggle>();
        //EventListener listener = itemGroupObj.AddComponent<EventListener>();
        //listener.eventAndResponses.Add(new EventAndResponse());
        //listener.eventAndResponses[0].gameEvent = inventoryToggle;
        //listener.eventAndResponses[0].responseForSentBool = new ResponseWithBool();
        //listener.eventAndResponses[0].responseForSentBool.AddListener(toggle.InventoryToggle);
        //CanvasGroup group = itemGroupObj.GetComponent<CanvasGroup>();
        //group.alpha = 0;
        //group.interactable = false;
        //group.blocksRaycasts = false;
        //itemGroupObj.transform.parent = transform;
        //itemGroupObj.transform.position = headImageTransform.position;

        //TODO: Add "Inventory Item" parent (itemParent) to contain each item frame, active frame, & item image.
        //for (int i = 0; i < objects.Count; i++)
        //{
        //    GameObject parent = Instantiate(itemParent, itemGroupObj.transform);
        //    //64 is the height of the item frame box. So I'm multiplying to space the next item box 
        //    //below the previous one, based on the increment in the loop.
        //    parent.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, i * -64f);

        //    //Set the image in the UI to the item's image.
        //    parent.transform.GetChild(parent.transform.childCount - 1).GetComponent<RawImage>().texture = objects[i].inventoryObject.inventoryImage;

        //    ////--Item Frame--
        //    //GameObject itemFrameCopy = Instantiate(itemFrame, itemGroupObj.transform.position, 
        //    //                    itemGroupObj.transform.rotation, itemGroupObj.transform);
        //    //RectTransform itemFrameTransform = itemFrameCopy.GetComponent<RectTransform>();
        //    //itemFrameTransform.anchorMin = anchorVector;
        //    //itemFrameTransform.anchorMax = anchorVector;
        //    //itemFrameTransform.anchoredPosition += new Vector2(18, -itemFrameTransform.rect.height * (i + 1.5f));
        //    ////--Item Frame--

        //    ////--Item Frame Active--
        //    //GameObject itemFrameActiveCopy = Instantiate(itemFrameActive, itemGroupObj.transform.position,
        //    //                    itemGroupObj.transform.rotation, itemGroupObj.transform);
        //    //RectTransform itemFrameActiveTransform = itemFrameActiveCopy.GetComponent<RectTransform>();
        //    //itemFrameActiveTransform.anchorMin = anchorVector;
        //    //itemFrameActiveTransform.anchorMax = anchorVector;
        //    //itemFrameActiveTransform.anchoredPosition += new Vector2(18, -itemFrameActiveTransform.rect.height * (i + 1.5f));
        //    ////--Item Frame Active--

        //    ////--Item Image--
        //    //GameObject itemImageCopy = Instantiate(itemImage, itemGroupObj.transform.position,
        //    //                    itemGroupObj.transform.rotation, itemGroupObj.transform);
        //    //RectTransform itemImageTransform = itemImageCopy.GetComponent<RectTransform>();
        //    //RawImage rawItem = itemImageCopy.GetComponent<RawImage>();
        //    //rawItem.texture = objects[i].inventoryObject.inventoryImage;
        //    //itemImageTransform.anchorMin = anchorVector;
        //    //itemImageTransform.anchorMax = anchorVector;
        //    //itemImageTransform.anchoredPosition += new Vector2(15, (-itemImageTransform.rect.height * (i + 1.5f)) - 0.5f);
        //    ////--Item Image--

        //    ////--Select Frame--
        //    //GameObject selectFrameCopy = Instantiate(selectFrame, itemGroupObj.transform.position,
        //    //                    itemGroupObj.transform.rotation, itemGroupObj.transform);
        //    //RectTransform selectFrameTransform = selectFrameCopy.GetComponent<RectTransform>();
        //    //selectFrameTransform.anchorMin = anchorVector;
        //    //selectFrameTransform.anchorMax = anchorVector;
        //    //selectFrameTransform.anchoredPosition += new Vector2(22, (-itemFrameTransform.rect.height * (i + 1.5f))-5);
        //    //--Select Frame--

        //}
    }

    //TODO: Add methods for adding and removing items from inventory & updating UI when this happens.

    //public void AddInventoryItem(ItemType type)
    //{
    //    switch (type)
    //    {

    //    }
    //}

    public void RemoveInventoryItem()
    {

    }
}

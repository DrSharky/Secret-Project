using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    public Sprite slotImage;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_ROWS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public Transform itemSpinParent;
    public TMPro.TMP_Text itemTitle;
    public TMPro.TMP_Text itemDescription;
    public Image invPic;
    public RawImage invSpinImage;

    private int NUMBER_OF_COLUMNS = 1;

    public override void CreateSlots()
    {
        NUMBER_OF_COLUMNS = Mathf.CeilToInt((float)inventory.ItemCount / (float)NUMBER_OF_ROWS);

        if(NUMBER_OF_COLUMNS > 1)
        {
            X_START = (int)(-(slotImage.rect.width/2)*(NUMBER_OF_COLUMNS-1));
        }

        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        
        for (int i = 0; i < inventory.ItemCount; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });

            inventory.GetSlots[i].slotDisplay = obj;

            //if(inventory.GetSlots[i].item.Id > -1)
            //{
            //    inventory.GetSlots[i].slotDisplay.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = inventory.GetSlots[i].GetItemObject().uiDisplay;
            //}

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }

    public override void UpdateSlots(int index)
    {
        int itmCount = inventory.ItemCount;

        for(int i = index; i <= itmCount; i++)
        {
            if (i < itmCount)
            {
                inventory.GetSlots[i].item = new Item(inventory.GetSlots[i + 1].GetItemObject());
                inventory.GetSlots[i].amount = (int)inventory.GetSlots[i + 1].amount;
                inventory.GetSlots[i].slotDisplay.transform.GetChild(0).GetComponent<Image>().sprite = inventory.GetSlots[i].GetItemObject().uiDisplay;
            }
            else
            {
                inventory.GetSlots[i].item = new Item();
                inventory.GetSlots[i].amount = 0;
                Destroy(inventory.GetSlots[i].slotDisplay);
            }
        }
    }

    public override void SetItemInfo(/*Item item*/GameObject obj)
    {
        Item item = slotsOnInterface[obj].item;

        foreach(GameObject slotObject in slotsOnInterface.Keys)
        {
            if(slotsOnInterface[slotObject].item != item)
            {
                slotObject.transform.GetChild(0).GetComponent<Image>().sprite = slotsOnInterface[slotObject].item.uiDisplay;
            }
            else
            {
                slotObject.transform.GetChild(0).GetComponent<Image>().sprite = slotsOnInterface[slotObject].item.uiDisplaySelect;
            }
        }

        for(int i = 0; i < itemSpinParent.childCount; i++)
        {
            DestroyImmediate(itemSpinParent.GetChild(i).gameObject);
        }
        GameObject inventoryObj = Instantiate(item.groundItem, itemSpinParent.position, Quaternion.identity, itemSpinParent);
        inventoryObj.transform.LookAt(inventoryObj.transform.parent.parent.position, Vector3.right);
        if (item.InvPic == null)
        {
            itemSpinParent.GetComponent<ItemSpin>().SetNewItem();
            if(invPic.sprite != null)
            {
                invPic.sprite = null;
                invPic.color = Color.clear;
                invSpinImage.color = Color.white;
            }
        }
        else
        {
            invPic.sprite = item.InvPic;
            invPic.color = Color.white;
            invSpinImage.color = Color.clear;
        }
        itemTitle.text = item.infoTitle;
        itemDescription.text = item.description;
    }

    public override GameObject AddSlot()
    {
        var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(inventory.ItemCount - 1);
        AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
        AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
        AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
        AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
        slotsOnInterface.Add(obj, inventory.GetSlots[inventory.ItemCount - 1]);
        return obj;
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_ROWS)),
            Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_ROWS)),
            0f);
    }
}

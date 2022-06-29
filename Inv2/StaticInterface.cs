using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;

    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Count; i++)
        {
            var obj = slots[i];


            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);

        }
    }

    public override void ChangeCategory(int index)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateSlots(int index)
    {
        return;
    }

    public override void SelectFirst()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject AddSlot()
    {
        return null;
    }
    public override void SetItemInfo(GameObject obj)
    {
        return;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIToggle : MonoBehaviour
{ 
    public CanvasGroup group;
    //bool inventoryActive;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void InventoryToggle(bool toggle)
    {
        if (toggle)
            group.alpha = 1;
        else
            group.alpha = 0;

        group.interactable = toggle;
        group.blocksRaycasts = toggle;
        //inventoryActive = !inventoryActive;
    }
}
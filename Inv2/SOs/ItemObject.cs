using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inv2/Items/New Item")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public Sprite uiDisplaySelect;
    public GameObject characterDisplay;
    public bool stackable;
    public bool canHaveMoreThanOne;
    //public Sprite image;
    public ItemType type;
    public Item data = new Item();

    public List<string> boneNames = new List<string>() {"Testing"};

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    private void OnValidate()
    {
        boneNames.Clear();
        if (characterDisplay == null)
            return;
        if (!characterDisplay.GetComponent<SkinnedMeshRenderer>())
            return;

        var renderer = characterDisplay.GetComponent<SkinnedMeshRenderer>();
        var bones = renderer.bones;

        foreach (var t in bones)
        {
            boneNames.Add(t.name);
        }
    }
    
}

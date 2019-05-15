using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Object")]
[System.Serializable]
public class InventoryObject : ScriptableObject
{
    public GameObject heldObject;
    public int heldQuantity;
    public Texture2D inventoryImage;
    public bool usable;
    public bool equippable;
    public bool canDrop;
    public ItemType type;
}

public enum ItemType
{
    None,
    General,
    Quest,
    Firearm,
    Melee,
    Clothing
}
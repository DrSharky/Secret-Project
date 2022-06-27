//TODO: UNCOMMENT WHEN USING ORIGINAL INVENTORY SYSTEM.
//COMMENTED TO TEST OUT PREMADE ONE.

//using UnityEngine;

//[CreateAssetMenu(menuName = "Inventory Object")]
//[System.Serializable]
//public class InventoryObject : ScriptableObject
//{
//    public void OnEnable()
//    {
//        if(uid == null)
//            uid = System.Guid.NewGuid().ToString();
//    }

//    public string uid;
//    public GameObject heldObject;
//    public Texture2D inventoryImage;
//    public bool usable;
//    public bool equippable;
//    public bool canDrop;
//    public ItemType type;
//    public string infoName;
//    [TextArea]
//    public string infoDescription;
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clothing Object", menuName = "Inv2/Items/Clothing")]
public class ClothingObject : ItemObject
{

    public GameObject clothingModel;

    public void Awake()
    {
        type = ItemType.Clothing;
    }
}

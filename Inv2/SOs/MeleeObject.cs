using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Object", menuName = "Inv2/Items/Melee")]
public class MeleeObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Melee;
    }
}
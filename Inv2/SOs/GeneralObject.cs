using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New General Object", menuName = "Inv2/Items/General")]
public class GeneralObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.General;
    }
}

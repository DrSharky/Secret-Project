using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour, IEquippable
{
    [SerializeField]
    public Mesh playerMesh;
    public void OnEquip()
    {
        PlayerEquippedItems.EquipClothes(this);
    }

    public void OnUnequip()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateEquipped()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquippedItems : MonoBehaviour
{
    [SerializeField]
    GameObject playerModelObject;
    static SkinnedMeshRenderer _renderer;
    //public static Clothes equippedClothes;

    //public static void EquipClothes(Clothes clothes)
    //{
    //    _renderer.sharedMesh = clothes.playerMesh;
    //}

    // Start is called before the first frame update
    void Start()
    {
        _renderer = playerModelObject.GetComponent<SkinnedMeshRenderer>();
    }
}

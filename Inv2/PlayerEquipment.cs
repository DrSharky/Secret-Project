using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private InventoryObject _equipment;
    
    [Header("Equip Transforms")]
    [SerializeField]private Transform offhandWristTransform;
    [SerializeField]private Transform offhandHandTransform;
    [SerializeField]private Transform weaponTransform;

    public bool source;
    
    private BoneCombiner _boneCombiner;
    private ClothingReplacer _clothingReplacer;
    private Transform _pants;
    private Transform _gloves;
    private Transform _boots;
    private Transform _chest;
    private Transform _helmet;
    private Transform _offhand;
    private Transform _sword;

    void Start()
    {
        _equipment = GetComponent<PlayerInv>().equipment;
        
        _boneCombiner = new BoneCombiner(gameObject);
        _clothingReplacer = new ClothingReplacer(gameObject);

        for (int i = 0; i < _equipment.GetSlots.Count; i++)
        {
            _equipment.GetSlots[i].onBeforeUpdated += OnRemoveItem;
            _equipment.GetSlots[i].onAfterUpdated += OnEquipItem;
        }
    }

    private void OnEquipItem(InventorySlot slot)
    {
        var itemObject = slot.GetItemObject();
        if (itemObject == null)
            return;
        switch (slot.parent.inventory.type)
        {
            case InterfaceType.Equipment:

                if (itemObject.characterDisplay != null)
                {
                    switch (slot.AllowedItems[0])
                    {
                        case ItemType.Clothing:
                            if (source)
                            {
                                ClothingObject clothingObj = (ClothingObject)itemObject;
                                _clothingReplacer.Replace(clothingObj.clothingModel);
                            }
                            else
                            {
                                _chest = _boneCombiner.AddLimb(itemObject.characterDisplay, itemObject.boneNames);
                            }
                            break;

                        case ItemType.Firearm:
                            switch (itemObject.type)
                            {
                                case ItemType.Firearm:
                                    _offhand = Instantiate(itemObject.characterDisplay, offhandWristTransform)
                                        .transform;
                                    break;
                                case ItemType.Melee:
                                    _offhand = Instantiate(itemObject.characterDisplay, offhandHandTransform)
                                        .transform;
                                    break;
                            }
                            break;

                        case ItemType.Melee:
                            _sword = Instantiate(itemObject.characterDisplay, weaponTransform).transform;
                            break;
                    }
                }
                break;
        }
    }

    private void OnRemoveItem(InventorySlot slot)
    {
        if (slot.GetItemObject() == null)
            return;
        switch (slot.parent.inventory.type)
        {
            case InterfaceType.Equipment:
                if (slot.GetItemObject().characterDisplay != null)
                {
                    switch (slot.AllowedItems[0])
                    {       
                        case ItemType.Clothing:
                            Destroy(_chest.gameObject);
                            break;
                        
                        case ItemType.Firearm:
                            Destroy(_offhand.gameObject);
                            break;
                        
                        case ItemType.Melee:
                            Destroy(_sword.gameObject);
                            break;
                    }
                }
                break;
        }
    }
}
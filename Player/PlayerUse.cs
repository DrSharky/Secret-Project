using UnityEngine;
using System.Linq;

public class PlayerUse : MonoBehaviour
{
    public Transform physObjParent;
    public Rigidbody playerRB;
    //public PlayerInventory playerInventory;

    //1024 = 1 << 10. Raycast should only cast on Interactables layer.
    //private int layerMask = 1024;
    private Interactable interactableObject;
    private string useString = "Use";
    private string interactableString = "Interactable";
    private string inventoryString = "InventoryObj";
    UnityEngine.GameFoundation.Inventory playerInv;
    [SerializeField]
    PlayerInventoryManager inventoryManager;

    public LayerMask useMask;

    void Start()
    {
        //playerInv = UnityEngine.GameFoundation.InventoryManager.GetInventory("main");
    }

    public void Unfreeze()
    {
        playerRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Freeze()
    {
        playerRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        //}

        //TODO: Replace using this static "usingComputer" variable with a generic variable
        //      that should be true/false when user can/can't activate object.
        if (Input.GetButtonDown(useString) && !Computer.usingComputer)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f, useMask) &&
                hit.collider.gameObject.tag.Equals(interactableString, System.StringComparison.Ordinal))
            {
                GameObject intGO = hit.collider.gameObject;
                interactableObject = intGO.GetComponent<Interactable>();
                if (interactableObject == null)
                    interactableObject = intGO.GetComponentInChildren<Interactable>();
                //Debug.DrawLine(hit.point, playerCam.position, Color.green, 10.0f);
                if (Time.timeScale > 0f)
                {
                    //if (interactableObject.freezePlayer)
                    //{
                    //    if (playerRB.constraints == RigidbodyConstraints.FreezeAll)
                    //        playerRB.constraints = RigidbodyConstraints.FreezeRotation;
                    //    else
                    //        playerRB.constraints = RigidbodyConstraints.FreezeAll;

                    //}
                    //RigidbodyFirstPersonController.frozen = true;

                    if (interactableObject.GetType() == typeof(PhysObj))
                    {
                        PhysObj physObject = (PhysObj)interactableObject;
                        if (!physObject.playerCarrying)
                            EventManager.TriggerEvent("Activate " + interactableObject.gameObject.name);
                        else
                            EventManager.TriggerEvent("Drop " + interactableObject.gameObject.name);
                    }
                    else if (interactableObject.GetType() == typeof(EventInteractable))
                        EventManager.TriggerEvent("Activate " + interactableObject.gameObject.name);
                    else
                        interactableObject.Activate();
                }
            }
            else if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f) &&
                      hit.collider.gameObject.tag.Equals(inventoryString, System.StringComparison.Ordinal))
            {
                GameObject invGO = hit.collider.gameObject;
                try
                {
                    //playerInv.AddItem(invGO.GetComponent<InventoryObjectScript>().id);
                    InventoryObject objToAdd = invGO.GetComponent<InventoryObjectScript>().inventoryObj;
                    string hitName = hit.collider.name;
                    AddToInventory(objToAdd, hitName);
                    Destroy(invGO);
                }
                catch(System.Exception ex)
                {
                    Debug.LogError("Inventory exception: " + ex.Message);
                }
            }
            else
            {
                if (physObjParent == null)
                    return;
                if (physObjParent.childCount > 0)
                {
                    PhysObj heldObj = physObjParent.GetChild(0).gameObject.GetComponent<PhysObj>();
                    heldObj.DropLogic();
                }
                //Debug.DrawRay(playerCam.position, playerCam.forward*2, Color.yellow, 10.0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !Computer.usingComputer)
        {
            if (RigidbodyFirstPersonController.frozen)
                RigidbodyFirstPersonController.frozen = false;
            else
                Application.Quit();
        }
    }

    void AddToInventory(/*Player*/InventoryObject objToAdd, string hitName)
    {
        //PlayerInventoryObject pio;

        //switch (objToAdd.type)
        //{
        //    case ItemType.Clothing:

        //        pio = playerInventory.clothingItems.Find(x => x.inventoryObject == objToAdd);

        //        if (pio != null)
        //        {
        //            //Add UI event to notify player they already have this clothing set.
        //            //They shouldn't have a duplicate!
        //        }
        //        else
        //        {
        //            pio = new PlayerInventoryObject(objToAdd, 1);
        //            playerInventory.clothingItems.Add(pio);
        //        }
        //        break;
        //    case ItemType.Firearm:

        //        pio = playerInventory.firearmItems.Find(x => x.inventoryObject == objToAdd);

        //        if (pio != null)
        //        {
        //            /*playerInventory.firearmItems.FirstOrDefault(x => x.name == hitName).heldQuantity++;*/
        //            //Add logic to increase ammo count if you pick up another gun of the same type.
        //            //The ammo increase logic should also dictate whether your ammo is full on that gun type or not.
        //        }
        //        else
        //        {
        //            pio = new PlayerInventoryObject(objToAdd, 1);
        //            playerInventory.firearmItems.Add(pio);
        //        }
        //        break;
        //    case ItemType.General:

        //        pio = playerInventory.generalItems.Find(x => x.inventoryObject == objToAdd);

        //        if (pio != null)
        //            playerInventory.generalItems.FirstOrDefault(x => x.inventoryObject.name == hitName).amountHeld++;
        //        else
        //        {
        //            pio = new PlayerInventoryObject(objToAdd, 1);
        //            playerInventory.generalItems.Add(pio);
        //        }
        //        break;
        //    case ItemType.Melee:

        //        pio = playerInventory.meleeItems.Find(x => x.inventoryObject == objToAdd);

        //        if (pio != null)
        //        {
        //            //Add UI event to notify player they already have this weapon.
        //            //They shouldn't have a duplicate!
        //        }
        //        else
        //        {
        //            pio = new PlayerInventoryObject(objToAdd, 1);
        //            playerInventory.meleeItems.Add(pio);
        //        }
        //        break;
        //    case ItemType.Quest:

        //        pio = playerInventory.questItems.Find(x => x.inventoryObject == objToAdd);

        //        if (pio != null)
        //        {
        //            //Add UI event to notify player they already have this quest item.
        //            //They shouldn't have a duplicate!
        //        }
        //        else
        //        {
        //            pio = new PlayerInventoryObject(objToAdd, 1);
        //            playerInventory.questItems.Add(pio);
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }
}
using UnityEngine;
using System.Linq;

public class PlayerUse : MonoBehaviour
{
    public Transform physObjParent;
    public Rigidbody playerRB;
    //public PlayerInventory playerInventory;
    public InventoryObject inventory, equipment;
    public InventoryDatabase inventories;
    public GameObject inventoryCanvas;
    public GameEvent inventoryToggle;
    //1024 = 1 << 10. Raycast should only cast on Interactables layer.
    //private int layerMask = 1024;
    private Interactable interactableObject;
    private string useString = "Use";
    private string interactableString = "Interactable";
    private string inventoryString = "InventoryObj";
    private const string INVENTORY = "Inventory";
    public static bool inventoryActive = false;
    //UnityEngine.GameFoundation.Inventory playerInv;
    //[SerializeField]
    //PlayerInventoryManager inventoryManager;

    public LayerMask useMask;

#if !UNITY_EDITOR
    void Awake()
    {
        inventory.Load();
    }
#endif


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

    public void InventoryToggle()
    {
        if (!inventoryActive)
        {
            Freeze();
            Time.timeScale = 0f;
            inventoryCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Unfreeze();
            Time.timeScale = 1f;
            inventoryCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        inventoryActive = !inventoryActive;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        //}
        if (Input.GetButtonDown(INVENTORY) && !Computer.usingComputer)
        {
            inventoryToggle.Raise();
        }

        //TODO: Replace using this static "usingComputer" variable with a generic variable
        //      that should be true/false when user can/can't activate object.
        if (Input.GetButtonDown(useString) && !Computer.usingComputer && Time.timeScale > 0f)
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
                    string hitName = hit.collider.name;
                    GroundItem hitItem = hit.collider.GetComponent<GroundItem>();
                    if (hitItem)
                    {
                        if(inventories.database[hitItem.item.data.category].AddItem(new Item(hitItem.item), 1))
                        {
                            Destroy(invGO);
                        }
                        //if (inventory.AddItem(new Item(hitItem.item), 1))
                        //{
                        //    Destroy(invGO);
                        //}
                    }
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

    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.KeypadPeriod))
        {
            inventory.Save();
            equipment.Save();
        }
        else if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            inventory.Load();
            //equipment.Load();
        }
    }

    public void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}
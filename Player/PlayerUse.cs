using System;
using UnityEngine;
using System.Linq;

public class PlayerUse : MonoBehaviour
{
    public Transform physObjParent;

    public PlayerInventory playerInventory;

    //1024 = 1 << 10. Raycast should only cast on Interactables layer.
    private int layerMask = 1024;
    private Interactable interactableObject;
    private string useString = "Use";
    private string interactableString = "Interactable";
    private string inventoryString = "InventoryObj";

    void Update()
    {
        if (Input.GetButtonDown(useString) && !Computer.usingComputer)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f) &&
                hit.collider.gameObject.tag.Equals(interactableString, System.StringComparison.Ordinal))
            {
                GameObject intGO = hit.collider.gameObject;
                interactableObject = intGO.GetComponent<Interactable>();
                if (interactableObject == null)
                    interactableObject = intGO.GetComponentInChildren<Interactable>();
                //Debug.DrawLine(hit.point, playerCam.position, Color.green, 10.0f);
                if (Time.timeScale > 0f)
                {
                    if (interactableObject.freezePlayer)
                        RigidbodyFirstPersonController.frozen = true;

                    if(interactableObject.GetType() == typeof(PhysObj))
                    {
                        PhysObj physObject = (PhysObj)interactableObject;
                        if (!physObject.playerCarrying)
                            EventManager.TriggerEvent("Activate" + interactableObject.gameObject.name);
                        else
                            EventManager.TriggerEvent("Drop" + interactableObject.gameObject.name);
                    }
                    else
                        EventManager.TriggerEvent("Activate" + interactableObject.gameObject.name);
                }
            }
            else if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f) &&
                      hit.collider.gameObject.tag.Equals(inventoryString, System.StringComparison.Ordinal))
            {
                GameObject invGO = hit.collider.gameObject;
                try
                {
                    InventoryObject objToAdd = hit.collider.gameObject.GetComponent<InventoryObjectScript>().inventoryObj;
                    if (playerInventory.inventory.Contains(objToAdd))
                        playerInventory.inventory.FirstOrDefault(x => x.name == hit.collider.name).heldQuantity++;
                    else
                    {
                        objToAdd.heldQuantity = 1;
                        playerInventory.inventory.Add(objToAdd);
                    }
                    Destroy(hit.collider.gameObject);

                }
                catch(Exception ex)
                {
                    Debug.LogError("Inventory exception: " + ex.Message);
                }
            }
            else
            {
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUse : MonoBehaviour
{

    public Transform playerCam;

    //1024 = 1 << 10. Raycast should only cast on Interactables layer.
    private int layerMask = 1024;
    private IInteractable interactableObject;
    private string useString = "Use";
    private string interactableString = "Interactable";

    void Update()
    {
        if (Input.GetButtonDown(useString) && !Computer.usingComputer)
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, 2.0f, layerMask) &&
                (hit.collider.gameObject.tag.Equals(interactableString, System.StringComparison.Ordinal)))
            {
                interactableObject = hit.collider.gameObject.GetComponent<IInteractable>();
                Debug.DrawLine(hit.point, playerCam.position, Color.green, 10.0f);
                if (Time.timeScale > 0f)
                {
                    if (interactableObject.freezePlayer)
                        RigidbodyFirstPersonController.frozen = true;

                    interactableObject.Activate();
                }
            }
            else
            {
                Debug.DrawRay(playerCam.position, playerCam.forward*3, Color.yellow, 10.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (RigidbodyFirstPersonController.frozen)
                RigidbodyFirstPersonController.frozen = false;
            else
                Application.Quit();
        }
    }
}
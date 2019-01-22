using UnityEngine;

public class PlayerUse : MonoBehaviour
{
    //1024 = 1 << 10. Raycast should only cast on Interactables layer.
    private int layerMask = 1024;
    private Interactable interactableObject;
    private string useString = "Use";
    private string interactableString = "Interactable";

    void Update()
    {
        if (Input.GetButtonDown(useString) && !Computer.usingComputer)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f) &&
                (hit.collider.gameObject.tag.Equals(interactableString, System.StringComparison.Ordinal)))
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

                    EventManager.TriggerEvent("Activate" + interactableObject.gameObject.name);
                }
            }
            else
            {
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
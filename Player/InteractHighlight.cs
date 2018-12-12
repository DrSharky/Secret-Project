using System.Collections.Generic;
using UnityEngine;

//Note: This script doesn't check if the player spawns in a scene
//with an interactable object already in range.
public class InteractHighlight : MonoBehaviour
{
    List<GameObject> gameObjects = new List<GameObject>();

    //1024 = 1 << 10. Raycast should only cast on Camera layer.
    private int layerMask = 1024;
    
    void Update()
    {
        if (gameObjects.Count == 0 || RigidbodyFirstPersonController.frozen)
            return;

        for(int i = 0; i < gameObjects.Count; i++)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 2.0f, layerMask))
            {
                Debug.DrawLine(hit.point, transform.position, Color.blue, 10.0f);
                Highlighter high = hit.collider.gameObject.GetComponentInChildren<Highlighter>();
                if(high != null)
                    high.highlighting = true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 2, Color.cyan, 10.0f);
                if(gameObjects.Count > 0)
                {
                    for(int j = 0; j < gameObjects.Count; j++)
                    {
                        Highlighter high = gameObjects[j].GetComponentInChildren<Highlighter>();
                        if (high != null)
                        {
                            high.highlighting = false;
                            high.SetColor(Color.black);
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            IInteractable interactable = other.gameObject.GetComponentInChildren<IInteractable>();
            if (interactable.highlight)
            {
                gameObjects.Add(other.gameObject);
                Highlighter high = other.gameObject.GetComponentInChildren<Highlighter>();
                if (high != null)
                    high.inRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            if (other.gameObject.GetComponentInChildren<IInteractable>().highlight && gameObjects.Contains(other.gameObject))
            {
                gameObjects.Remove(other.gameObject);
                Highlighter high = other.gameObject.GetComponentInChildren<Highlighter>();
                if (high != null)
                    high.inRange = false;
            }
        }
    }
}
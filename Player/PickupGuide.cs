using UnityEngine;

public class PickupGuide : MonoBehaviour
{
    public Transform cam;
    void Update()
    {
        transform.position = cam.position + (cam.forward*1.3f);
        transform.rotation = cam.rotation;
    }
}

//TODO: Make it so physics objects that are picked up don't collide with player. 
//TODO: Make physics objects that are picked up collide with walls & such, so player can't drop them outside.
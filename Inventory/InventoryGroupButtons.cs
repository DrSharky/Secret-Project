using UnityEngine;

public class InventoryGroupButtons : MonoBehaviour
{
    UnityEngine.UI.RawImage img;

    void Awake()
    {
        img = GetComponent<UnityEngine.UI.RawImage>();
    }

    public void InventoryToggle()
    {
        img.enabled = !img.enabled;
    }
}

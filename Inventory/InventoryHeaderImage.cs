using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHeaderImage : MonoBehaviour
{
    public List<Texture2D> headerImages;

    Color transparent = new Color(255, 255, 255, 0);
    Color solid = new Color(255, 255, 255, 255);
    RawImage headerImage;
    bool toggleInventory = false;
    int imageIndex;

    void Awake()
    {
        headerImage = GetComponent<RawImage>();
        imageIndex = 0;
        headerImage.texture = headerImages[imageIndex];
    }

    public void ToggleInventory()
    {
        if (!toggleInventory)
        {
            headerImage.texture = headerImages[0];
            headerImage.color = solid;
        }
        else
            headerImage.color = transparent;

        toggleInventory = !toggleInventory;
    }

    public void NextInventoryType()
    {
        if (imageIndex == headerImages.Count - 1)
            imageIndex = 0;
        else
            imageIndex++;

        headerImage.texture = headerImages[imageIndex];
    }

    public void PrevInventoryType()
    {
        if (imageIndex == 0)
            imageIndex = headerImages.Count - 1;
        else
            imageIndex--;

        headerImage.texture = headerImages[imageIndex];
    }
}

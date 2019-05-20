using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSpin : MonoBehaviour
{
    public bool inventoryActive = false;
    public Image spinPanel;
    public EventSystem eventSystem;
    public GraphicRaycaster raycaster;
    public GameEvent inventoryToggle;
    Vector3 rotOrigin;
    BoxCollider childCollider;
    Transform childTransform;
    Vector3 rotAxis;
    PointerEventData pointerData;
    Vector3 initMousePos = -Vector3.one;
    float mouseDistanceDelta;
    bool clickDown = false;

	// Use this for initialization
	void Start ()
    {
        GameObject inventoryItemModel = transform.GetChild(0).gameObject;
        childCollider = inventoryItemModel.GetComponent<BoxCollider>();
        childTransform = inventoryItemModel.transform;
    }

	// Update is called once per frame
	void Update ()
    {
        if (inventoryActive)
        {
            if (Input.GetMouseButton(0))
            {
                if (clickDown)
                {
                    float xRot = Input.GetAxis("Mouse X") * 12f;
                    rotOrigin = childTransform.TransformPoint(childCollider.center);
                    childTransform.RotateAround(rotOrigin, Vector3.up, -xRot);
                }
                else
                {
                    pointerData = new PointerEventData(eventSystem);
                    pointerData.position = Input.mousePosition;
                    List<RaycastResult> castResults = new List<RaycastResult>();
                    raycaster.Raycast(pointerData, castResults);
                    foreach (RaycastResult result in castResults)
                    {
                        if (result.gameObject.GetComponent<Image>() == spinPanel)
                            clickDown = true;
                    }
                }

                if (Input.GetMouseButtonDown(0) && clickDown)
                    initMousePos = Input.mousePosition;

                if (initMousePos != -Vector3.one)
                    mouseDistanceDelta = initMousePos.x - Input.mousePosition.x;

                if (!clickDown)
                {
                    rotOrigin = childTransform.TransformPoint(childCollider.center);
                    childTransform.RotateAround(rotOrigin, Vector3.up, 3);
                }
            }
            else
            {
                rotOrigin = childTransform.TransformPoint(childCollider.center);
                childTransform.RotateAround(rotOrigin, Vector3.up, 3);
                clickDown = false;
                initMousePos = -Vector3.one;
            }
        }
	}

    public void InventoryToggled()
    {
        inventoryActive = !inventoryActive;
        if (!inventoryActive)
            clickDown = false;
        initMousePos = -Vector3.one;
    }
}

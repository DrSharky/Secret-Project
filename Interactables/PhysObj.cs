using UnityEngine;
using System.Collections;

public class PhysObj : Interactable
{
    [Header("Physics Object Fields")]
    public GameObject item;
    public Transform guide;
    public Rigidbody rb;
    public bool playerCarrying = false;
    bool activateRunning = false;
    public System.Action dropListener { get; set; }

    WaitForEndOfFrame delay = new WaitForEndOfFrame();

    public override void Awake()
    {
        base.Awake();
        dropListener = new System.Action(() => DropLogic());
        EventManager.StartListening("Drop" + gameObject.name, dropListener);
    }

    IEnumerator Pickup()
    {
        if(!activateRunning)
        {
            activateRunning = true;
            playerCarrying = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            item.transform.position = guide.transform.position;
            item.transform.parent = guide;
        }
        yield return null;
    }

    IEnumerator DropRoutine()
    {
        DropLogic();
        yield return null;
    }

    public void DropLogic()
    {
        playerCarrying = false;
        item.transform.parent = null;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public override void Activate()
    {
        if (!playerCarrying)
            StartCoroutine(Pickup());
        else
            StartCoroutine(DropRoutine());
        activateRunning = false;
    }

    void Update()
    {
        if (playerCarrying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DropLogic();
                rb.AddForce(guide.transform.forward * 3.5f, ForceMode.Impulse);
            }
        }
    }
}

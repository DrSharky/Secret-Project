using UnityEngine;
using System.Collections;

public class PhysObj : EventInteractable
{
    [Header("Physics Object Fields")]

    public Transform playerCam;
    public GameObject item;

    //public Transform guide;
    public Rigidbody rb;
    public bool playerCarrying = false;
    //bool activateRunning = false;
    public System.Action dropListener { get; set; }
    WaitForEndOfFrame delay = new WaitForEndOfFrame();

    private float pickupRange = 3f, throwStrength = 50f, holdDistance = 3f, maxDistanceGrab = 4f;

    private Ray playerAim;

    public override void Awake()
    {
        base.Awake();
        dropListener = new System.Action(() => DropLogic());
        EventManager.StartListening("Drop " + gameObject.name, dropListener);
    }

    //Why am I using coroutines for this?
    IEnumerator Pickup()
    {
        //if(!activateRunning)
        //{
            //activateRunning = true;
            playerCarrying = true;
            rb.useGravity = false;
            //rb.isKinematic = true;
            //rb.isKinematic = false;
            //item.transform.position = guide.transform.position;
            //item.transform.parent = guide;
            //rb.freezeRotation = true;
        //}
        yield return null;
    }

    void ThrowObject()
    {
        rb.AddForce(playerCam.forward * throwStrength);
        rb.freezeRotation = false;
        playerCarrying = false;
    }

    IEnumerator DropRoutine()
    {
        DropLogic();
        yield return null;
    }

    public void DropLogic()
    {
        DropObject();
        playerCarrying = false;
        item.transform.parent = null;
        //rb.isKinematic = true;
        rb.useGravity = true;
        //rb.isKinematic = false;
        //rb.freezeRotation = false;
    }

    public override void Activate()
    {
        rb.detectCollisions = true;

        if (!playerCarrying)
            StartCoroutine(Pickup());
        else
            StartCoroutine(DropRoutine());
        //activateRunning = false;
    }

    void Update()
    {
        if (playerCarrying)
        {
            //item.transform.position = item.transform.parent.position;
            if (Input.GetMouseButtonDown(0))
            {
                //DropLogic();
                DropObject();
                //rb.AddForce(guide.transform.forward * 3.5f, ForceMode.Impulse);
            }
        }
    }

    Vector3 screenCenter = new Vector3(0.5f, 0.5f);

    void HoldObject()
    {
        playerAim = Camera.main.ViewportPointToRay(screenCenter);
        Vector3 nextPos = playerCam.position + playerAim.direction * holdDistance;
        Vector3 curPos = item.transform.position;

        rb.velocity = (nextPos - curPos) * 12;

        if (Vector3.Distance(item.transform.position, Camera.main.transform.position) > maxDistanceGrab)
        {
            DropObject();
        }

    }

    void DropObject()
    {
        playerCarrying = false;
        //item.transform.parent = null;
        rb.useGravity = true;
        rb.freezeRotation = false;
    }

    private void FixedUpdate()
    {
        if (playerCarrying)
        {
            HoldObject();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
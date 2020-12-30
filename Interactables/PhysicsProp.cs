using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsProp : Interactable
{
    [Header("Interaction Area Base")]
    #region InteractionAreaBase
    [Tooltip("If character need to look at this object with camera direction," +
        "useful for FPS games. Value = 1" +
        "Camera can look anywhere interaction always popup," +
        "Value = 0.25 camera must look almost directly on object" +
        " for interaction popup")]
    [Range(0.15f, 1f)]
    public float LookAtRange = 1f;

    /// <summary>
    /// Could have same value as CanvasObjectOffset in 
    /// FBasic_InteractionAreaCanvas for more precision
    /// </summary>
    protected Vector3 toLookPositionOffset = Vector3.zero;

    /// <summary>
    /// When doing animations in UpdateIn() method,
    /// object exits player range and you want still to update 
    /// until animation finish (for example door hinge animation)
    /// </summary>
    protected bool? conditionalExit = null;

    public SphereCollider triggerArea;

    public bool Entered;

    /// <summary>
    /// Changes every time the player enters/leaves trigger,
    /// ignoring dependencies 
    /// </summary>
    public bool EnteredFlag;

    /// <summary> 
    /// If we enter few interaction areas, 
    /// one we can have this variable set to true 
    /// </summary>
    public bool Focused;

    public Transform EnteredTransform;

    /// <summary> 
    /// Used when 'NeedToLookAt' is enabled, 
    /// helps to define which object is focused the most 
    /// </summary>
    protected float VisibleFactor;

    #endregion

    public bool Holding;

    [Header("< Draggable Parameters >")]

    public Rigidbody TargetRigidbody;

    [Range(0f, 5f)]
    public float HardFollow = 1f;
    [Range(0f, 1f)]
    public float FollowMassRatio = 1f;

    [Range(0f, 3f)]
    public float ThrowMultiplier = 1f;

    private Camera refCamera;
    private Vector3 holdOffset;
    private Vector3 preTargetPos;
    private Vector3 holdVelocity;
    private Quaternion holdCameraOrientation;
    public Collider rigColl;

    public Transform correctRot;

    private void Reset()
    {
        GetTrigger();
        TargetRigidbody = GetComponentInChildren<Rigidbody>();
        if (!TargetRigidbody)
            TargetRigidbody = GetComponentInParent<Rigidbody>();
    }

    protected void Start()
    {
        Holding = false;
        fixedDelay = new WaitForFixedUpdate();
        refCamera = Camera.main;
    }

    protected Collider GetTrigger()
    {
        SphereCollider[] colls = GetComponents<SphereCollider>();

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].isTrigger)
            {
                triggerArea = colls[i];
                break;
            }
        }
        return triggerArea;
    }

    PhysicsProp LockedInteraction;

    //IEnumerator RotateForHolding()
    //{
    //    Quaternion finalRot = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
    //    while(transform.rotation != finalRot)
    //    {
    //        transform.Rotate()
    //    }
    //    yield return null;
    //}

    protected virtual void StartHolding()
    {
        if (!Holding)
        {
            LockedInteraction = this;
            correctRot.rotation = Quaternion.LookRotation(-refCamera.transform.forward, Vector3.up);
            transform.rotation = Quaternion.LookRotation(-correctRot.right, Vector3.up);
            holdOffset = refCamera.transform.position - transform.position;
            holdOffset = Quaternion.LookRotation(holdOffset, refCamera.transform.forward) * Vector3.back * holdOffset.magnitude;
            holdCameraOrientation = refCamera.transform.rotation;
            Holding = true;
            StartCoroutine(pickupDelay());
        }
    }

    public override void Activate() {StartHolding();}

    WaitForSeconds pickupWait = new WaitForSeconds(0.1f);
    bool pickupCool;
    IEnumerator pickupDelay()
    {
        pickupCool = true;
        yield return pickupWait;
        pickupCool = false;
    }

    protected void UpdateIn()
    {
        if (Holding && !pickupCool)
        {
            if (Input.GetKeyDown(KeyCode.E)) StopHolding();
        }
    }

    private readonly float pow = 40f;
    private float lerpedDistBlender = 0.1f;
    private WaitForFixedUpdate fixedDelay;

    protected virtual IEnumerator UpdateInFixed()
    {
        while (true)
        {
            if (Holding)
            {
                correctRot.rotation = Quaternion.LookRotation(-refCamera.transform.forward, Vector3.up);
                transform.rotation = Quaternion.LookRotation(-correctRot.right, Vector3.up);
                Vector3 targetPosition = refCamera.transform.position + (refCamera.transform.rotation * Quaternion.Inverse(holdCameraOrientation)) * holdOffset;
                holdVelocity = Vector3.Lerp(holdVelocity, targetPosition - preTargetPos, Time.fixedDeltaTime * 15f);

                float rayLen;
                if (rigColl) rayLen = rigColl.bounds.extents.magnitude; else rayLen = 1f;

                Ray ray = new Ray(transform.position, targetPosition - transform.position);
                RaycastHit obstacle;
                Physics.Raycast(ray, out obstacle, ray.direction.magnitude * rayLen, ~0, QueryTriggerInteraction.Ignore);

                float dist = Vector3.Distance(TargetRigidbody.position, targetPosition);
                float distPower = Mathf.Lerp(1f, 0.1f, Mathf.InverseLerp(rigColl.bounds.size.magnitude, 0.1f, dist));

                Vector3 targetSmoothPos = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * (15f * HardFollow) / Mathf.Lerp(1f, (0.25f + TargetRigidbody.mass / 1.5f), FollowMassRatio));

                float moveLerper = 0f;

                if (obstacle.transform)
                {
                    moveLerper = Mathf.Lerp(0.75f, 1f, Mathf.InverseLerp(1f, 0.1f, distPower));
                    distPower /= 2.5f;
                    //Debug.Log("OBST! lerper = " + moveLerper + " distpow = " + distPower);
                }
                else
                {
                    if (distPower < 0.5f)
                    {
                        moveLerper = Mathf.Lerp(.5f, 0.1f, Mathf.InverseLerp(1f, 0.5f, distPower));
                    }
                    else
                    {
                        moveLerper = Mathf.Lerp(0.0f, 1f, Mathf.InverseLerp(0.5f, 0.01f, distPower));
                    }
                }

                lerpedDistBlender = Mathf.Lerp(lerpedDistBlender, moveLerper, Time.fixedDeltaTime * 10f);

                TargetRigidbody.MovePosition(Vector3.Lerp(targetSmoothPos, TargetRigidbody.position, lerpedDistBlender));

                Vector3 finalForce = ((targetPosition - TargetRigidbody.transform.position) * HardFollow * pow) / (Mathf.Max(1f, (TargetRigidbody.mass * FollowMassRatio) / 3f));
                TargetRigidbody.AddForce(Vector3.Lerp(finalForce, Vector3.zero, distPower));

                TargetRigidbody.useGravity = false;
                TargetRigidbody.angularVelocity = Vector3.Lerp(TargetRigidbody.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 1f);

                preTargetPos = targetSmoothPos;

                // If we go too far from dragged object when we stuck it in something
                //if (Vector3.Distance(transform.position, refCamera.transform.position) > triggerArea.bounds.extents.magnitude * 2f)
                //{
                //    StopHolding();
                //}
            }
            yield return fixedDelay;
        }
    }

    public List<PhysicsProp> EnteredInteractions = new List<PhysicsProp>();
    public PhysicsProp LastEnteredInteraction = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnteredFlag = true;
        }

        if (Entered) return;

        if (EnteredFlag)
        {
            EnteredTransform = other.transform;
            StartCoroutine(UpdateIfInRange());
            OnEnter();
        }
    }

    protected virtual void OnEnter()
    {
        Entered = true;
        EnteredInteractions.Add(this);
        LastEnteredInteraction = this;
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            EnteredFlag = false;
        }

        if (!Entered) return;

        if (EnteredFlag == false)
        {
            if (conditionalExit != true)
            {
                if (LockedInteraction != this)
                {
                    StopAllCoroutines();
                    OnExit();
                }
            }
        }
    }

    protected virtual void OnExit()
    {
        Entered = false;
        EnteredTransform = null;
        EnteredInteractions.Remove(this);

        if (LastEnteredInteraction == this)
        {
            LastEnteredInteraction = null;
            if (EnteredInteractions.Count > 0) LastEnteredInteraction = EnteredInteractions[EnteredInteractions.Count - 1];
        }
    }

    private void OnDestroy()
    {
        if (LockedInteraction == this) UnlockInteraction();
    }

    public void UnlockInteraction()
    {
        if (LockedInteraction != null)
        {
            if (!LockedInteraction.Entered)
            {
                LockedInteraction.StopAllCoroutines();
                LockedInteraction.OnExit();
            }
        }
        LockedInteraction = null;
    }

    /// <summary>
    /// Resetting values for holding object
    /// </summary>
    protected virtual void StopHolding()
    {
        if (LockedInteraction == this) UnlockInteraction();

        lerpedDistBlender = 0.1f;
        Holding = false;
        TargetRigidbody.constraints = RigidbodyConstraints.None;
        TargetRigidbody.useGravity = true;
        TargetRigidbody.velocity = (holdVelocity * (10f * HardFollow) / Mathf.Lerp(1f, (0.1f + TargetRigidbody.mass), FollowMassRatio)) * ThrowMultiplier;

        if (!EnteredFlag)
        {
            conditionalExit = false;
            OnTriggerExit(EnteredTransform.GetComponent<Collider>());
        }
    }

    protected IEnumerator UpdateIfInRange()
    {
        StartCoroutine("UpdateInFixed");
        yield return UpdateBase();
    }

    private IEnumerator UpdateBase()
    {
        while (true)
        {
            Focused = false;

            if (LockedInteraction == null)
            {
                // Letting one unique object to be marked as focused
                if (LookAtRange >= 1f)
                {
                    if (LastEnteredInteraction == this) Focused = true;
                }
                else
                {
                    // If object's position point is visible in camera view
                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position + transform.TransformVector(toLookPositionOffset));

                    if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
                    {
                        float xFactor = screenPoint.x; xFactor = Mathf.Abs(0.5f - xFactor);
                        float yFactor = screenPoint.y; yFactor = Mathf.Abs(0.5f - yFactor);

                        // Calculating looking at factor
                        VisibleFactor = (xFactor + yFactor);

                        // Checking which entered interaction zone is focused the most
                        if (VisibleFactor < LookAtRange)
                        {
                            if (EnteredInteractions.Count > 1)
                            {
                                float mostFocused = EnteredInteractions[0].VisibleFactor;
                                int mostFocusedI = 0;

                                for (int i = 1; i < EnteredInteractions.Count; i++)
                                {
                                    if (EnteredInteractions[i].VisibleFactor < mostFocused)
                                    {
                                        mostFocused = EnteredInteractions[i].VisibleFactor;
                                        mostFocusedI = i;
                                    }
                                }

                                if (EnteredInteractions[mostFocusedI] == this) Focused = true;
                            }
                            else
                                Focused = true;
                        }
                    }
                }
            }
            else // If we lock interaction, for example when we using doors or something, we can focus only on this one object
            {
                if (LockedInteraction == this) Focused = true;
            }

            // Running update loop when needed
            UpdateIn();

            // Exit conducted by custom script actions
            if (conditionalExit == false)
            {
                if (EnteredFlag == false)
                {
                    conditionalExit = null;
                    OnExit();
                    StopAllCoroutines();
                }
            }

            yield return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHands : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Update()
    {
        secondHand.Rotate(Vector3.right, -6f * Time.deltaTime);
        minuteHand.Rotate(Vector3.right, -0.1f * Time.deltaTime);
        hourHand.Rotate(Vector3.right, -((6f/360f)/2) * Time.deltaTime);
    }
}

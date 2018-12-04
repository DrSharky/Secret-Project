using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsRotate : MonoBehaviour
{
    [SerializeField]
    private float rate = 10.0f;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, rate * Time.deltaTime, 0);
    }
}

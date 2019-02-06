using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    private NavMeshAgent agent;

	// Use this for initialization
	void Start()
	{
        agent = GetComponent<NavMeshAgent>();
	}
}
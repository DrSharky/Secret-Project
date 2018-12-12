using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private bool freeze = true;
    public bool freezePlayer { get { return freeze; } set { freeze = value; } }
    private bool highlightable = false;
    public bool highlight { get { return highlightable; } set { highlightable = value; } }

    GameObject player;
    Animator anim;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
	}

    public void Activate()
    {
        Vector3 newDir = player.transform.position - transform.position;
        newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);
        anim.SetTrigger("Talk");
    }
}

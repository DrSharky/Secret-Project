using UnityEngine;

public class NPC : Interactable
{
    private string animTrigger = "Talk";
    public AudioClip[] dialogues;
    public GameObject player;
    private Animator anim;

	void Start ()
    {
        anim = GetComponentInChildren<Animator>();
	}

    public override void Activate()
    {
        Vector3 newDir = player.transform.position - transform.position;
        newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);
        anim.SetTrigger(animTrigger);
    }
}

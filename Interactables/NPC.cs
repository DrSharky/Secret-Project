using UnityEngine;

public class NPC : Interactable
{
    private string animTrigger = "Intro";
    public AudioClip[] dialogues;
    public GameObject player;
    private Animator anim;
    RogoDigital.Lipsync.LipSync LS;
    public RogoDigital.Lipsync.LipSyncData LSData;   


	void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        LS = GetComponent<RogoDigital.Lipsync.LipSync>();
	}

    public override void Activate()
    {
        Vector3 newDir = player.transform.position - transform.position;
        newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);
        LS.Play(LSData);
        anim.SetTrigger(animTrigger);
    }
}

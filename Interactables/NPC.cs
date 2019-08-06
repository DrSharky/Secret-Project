using UnityEngine;
using RogoDigital.Lipsync;

public class NPC : Interactable
{
    //This is just a test thing, will remove later.
    private string animTrigger = "Intro";
    public AudioClip[] dialogues;
    public GameObject player;
    private Animator anim;
    LipSync LS;
    public LipSyncData LSData;
    public NPCState state;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        LS = GetComponent<LipSync>();
	}

    public override void Activate()
    {
        Vector3 newDir = player.transform.position - transform.position;
        newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);
        LS.Play(LSData);
        anim.SetTrigger(animTrigger);
    }

    void Update()
    {
        if(state == NPCState.Dead)
            Destroy(this);
    }
}

public enum NPCState
{
    None = 0,
    Idle = 1,
    Wandering = 2,
    Talking = 3,
    Engaged = 4,
    Running = 5,
    Attacking = 6,
    Stunned = 7,
    Dead = 8
}
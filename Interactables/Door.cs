using UnityEngine;

/// <summary>
/// Any kind of door that doesn't change maps.
/// </summary>
public class Door : Interactable
{

    public AudioClip openSound;
    public AudioClip closeSound;

    public AudioSource doorAudio;
    private bool doorClosed = true;

    private bool opening = false;
    private bool closing = false;

    private bool closePlayed = false;

    private Quaternion openRot;
    private Quaternion closedRot;

    private Vector3 openPos;
    private Vector3 closedPos;
    private Vector3 targetDir;

    // Use this for initialization
    void Start ()
    {
        highlight = false;
        doorAudio = GetComponent<AudioSource>();
        doorAudio.clip = openSound;
        openRot = transform.rotation * Quaternion.Euler(0.0f, 90.0f, 0.0f);
        closedRot = transform.rotation;

        openPos = new Vector3(90.0f, 90.0f, 90.0f);
        closedPos = transform.forward;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (opening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, 2.5f * Time.deltaTime);

            if (transform.rotation == openRot)
            {
                opening = false;
                doorClosed = false;
            }
        }
        if (closing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRot, 0.065f);
            float soundAngle = Quaternion.Angle(transform.rotation, closedRot);
            if (soundAngle < 3.0f && !closePlayed)
            {
                doorAudio.clip = closeSound;
                doorAudio.Play();
                closePlayed = true;
            }
            if (transform.rotation == closedRot)
            {
                closing = false;
                doorClosed = true;
                doorAudio.clip = openSound;
            }
        }
    }

    public override void Activate()
    {
        if (doorClosed && !opening)
        {
            targetDir = openPos - transform.position;
            opening = true;
            closing = false;
            if (doorAudio.clip == closeSound)
                doorAudio.clip = openSound;
            doorAudio.Play();
            closePlayed = false;
        }
        else
        {
            targetDir = closedPos - transform.position;
            opening = false;
            closing = true;
        }
    }
}

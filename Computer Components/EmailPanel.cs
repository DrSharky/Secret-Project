using UnityEngine;

public class EmailPanel : ComputerPanel, IEmailPanel
{
    public System.Action<bool> EmailPanelListener { get; set; }

    public GameObject[] Emails;

    public void Awake()
    {
        GenerateActivationEvent(EmailPanelListener, EventManager.emailPanelToggle, true);
    }

    //Leave this empty so it overrides the ComputerPanel
    //Start and doesn't listen for Exit Screen event.
    public override void Start(){}

    public override void OnDestroy()
    {
        base.OnDestroy();
        StopEventListening(EventManager.emailPanelToggle, EmailPanelListener);
    }

    public override void ActivatePanel()
    {
        gameObject.SetActive(true);
    }

    public override void DeactivatePanel()
    {
        gameObject.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PCEmailCommand
{
    public bool read;
    public bool showEmail;
    public EmailCommand emailCommand;

    public PCEmailCommand(EmailCommand cmd = null)
    {
        emailCommand = cmd;
        showEmail = true;
    }
}

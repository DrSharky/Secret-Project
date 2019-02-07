using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Computer Game Event")]
public class ComputerGameEvent : GameEvent
{

    public ScreenType sentScreenType;

    public enum ScreenType
    {
        None,
        Normal,
        Password,
        PasswordFail,
        Email,
        EmailMenu,
        DisplayText
    }

    private List<ComputerEventListener> computerEventListeners = new List<ComputerEventListener>();

    public override void Raise()
    {
        base.Raise();
    }

    public void Register(ComputerEventListener passedEvent)
    {

        if (!computerEventListeners.Contains(passedEvent))
        {
            computerEventListeners.Add(passedEvent);
        }

    }

    public void DeRegister(ComputerEventListener passedEvent)
    {

        if (computerEventListeners.Contains(passedEvent))
        {
            computerEventListeners.Remove(passedEvent);
        }

    }

}
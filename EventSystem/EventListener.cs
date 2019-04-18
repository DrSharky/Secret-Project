using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [Sirenix.OdinInspector.ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
    public List<EventAndResponse> eventAndResponses = new List<EventAndResponse>();

    private void OnEnable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (EventAndResponse eAndR in eventAndResponses)
            {
                if(eAndR.gameEvent == null)
                {
                    Debug.Log("eAndR Error: " + eAndR);
                    Debug.Log("Missing Game Event on listener: " + gameObject.name);
                }
                else
                    eAndR.gameEvent.Register(this);

            }
        }

    }
    private void OnDisable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (EventAndResponse eAndR in eventAndResponses)
            {
                if (eAndR.gameEvent != null)
                    eAndR.gameEvent.DeRegister(this);
            }
        }
    }

    [ContextMenu("Raise Events")]
    public void OnEventRaised(GameEvent passedEvent)
    {
        for (int i = eventAndResponses.Count - 1; i >= 0; i--)
        {
            // Check if the passed event is the correct one
            if (passedEvent == eventAndResponses[i].gameEvent)
            {
                // Uncomment the line below for debugging the event listens and other details
                //Debug.Log("Called Event named: " + eventAndResponses[i].name +
                //          " on GameObject: " + gameObject.name);


                eventAndResponses[i].EventRaised();
            }
        }
    }
}

[System.Serializable]
public class EventAndResponse
{
    public string name;
    public GameEvent gameEvent;
    public UnityEvent response;
    public ResponseWithString responseForSentString;
    public ResponseWithInt responseForSentInt;
    public ResponseWithFloat responseForSentFloat;
    public ResponseWithBool responseForSentBool;
    public ResponseWithScreenType responseForScreenType;

    public virtual void EventRaised()
    {
        // default/generic
        if (response.GetPersistentEventCount() >= 1) // always check if at least 1 object is listening for the event
        {
            response.Invoke();
        }

        // string
        if (responseForSentString.GetPersistentEventCount() >= 1)
        {
            responseForSentString.Invoke(gameEvent.sentString);
        }

        // int
        if (responseForSentInt.GetPersistentEventCount() >= 1)
        {
            responseForSentInt.Invoke(gameEvent.sentInt);
        }

        // float
        if (responseForSentFloat.GetPersistentEventCount() >= 1)
        {
            responseForSentFloat.Invoke(gameEvent.sentFloat);
        }

        // bool
        if (responseForSentBool.GetPersistentEventCount() >= 1)
        {
            responseForSentBool.Invoke(gameEvent.sentBool);
        }

        // ScreenType
        if (responseForScreenType.GetPersistentEventCount() >= 1)
        {
            responseForScreenType.Invoke(gameEvent.sentScreenType);
        }
    }
}

[Serializable]
public class ResponseWithString : UnityEvent<string>
{
}

[Serializable]
public class ResponseWithInt : UnityEvent<int>
{
}

[Serializable]
public class ResponseWithFloat : UnityEvent<float>
{
}

[Serializable]
public class ResponseWithBool : UnityEvent<bool>
{
}

[Serializable]
public class ResponseWithScreenType : UnityEvent<ScreenType>
{
}
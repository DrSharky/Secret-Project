using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComputerEventListener : MonoBehaviour
{
    public List<ComputerEventAndResponse> eventAndResponses = new List<ComputerEventAndResponse>();

    private void OnEnable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (ComputerEventAndResponse eAndR in eventAndResponses)
            {
                eAndR.computerGameEvent.Register(this);
            }
        }


    }
    private void OnDisable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (ComputerEventAndResponse eAndR in eventAndResponses)
            {
                eAndR.computerGameEvent.DeRegister(this);
            }
        }
    }

    [ContextMenu("Raise Events")]
    public void OnEventRaised(ComputerGameEvent passedEvent)
    {

        for (int i = eventAndResponses.Count - 1; i >= 0; i--)
        {
            // Check if the passed event is the correct one
            if (passedEvent == eventAndResponses[i].gameEvent)
            {
                // Uncomment the line below for debugging the event listens and other details
                //Debug.Log("Called Event named: " + eventAndResponses[i].name + " on GameObject: " + gameObject.name);


                eventAndResponses[i].EventRaised();
            }
        }

    }
}

[System.Serializable]
public class ComputerEventAndResponse : EventAndResponse
{
    //use new to override the base eventAndResponses field.
    public ComputerGameEvent computerGameEvent;

    public ResponseWithScreenType responseForSentScreenType;

    public override void EventRaised()
    {        
        if (responseForSentScreenType.GetPersistentEventCount() >= 1)
        {
            responseForSentScreenType.Invoke(computerGameEvent.sentScreenType);
        }

    }
}

[System.Serializable]
public class ResponseWithScreenType : UnityEvent<ComputerGameEvent.ScreenType>
{
}
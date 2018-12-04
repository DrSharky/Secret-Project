using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour
{
    #region Event strings
    //For handling the hiding/showing of the display panel on Computers.
    //The end space is intentional, the event string is combined with the computer object name,
    //in case there are several computers in the same map.
    public static string displayPanelToggle = "DisplayPanel Event ";

    //For handling the hiding/showing of the menu panels on Computers. (Including command Input).
    //The end space is intentional, the event string is combined with the computer object name,
    //in case there are several computers in the same map.
    public static string menuPanelToggle = "MenuPanel Event ";

    public static string emailPanelToggle = "EmailPanel Event ";

    public static string exitScreenEvent = "ExitScreen Event ";

    public static string passwordPanelToggle = "PassPanel Event ";

    public static string titlePanelToggle = "TitlePanel Event ";
    #endregion

    private DictionaryByType dictByType;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                    Debug.LogError("Needs 1 active EventManager script on a GO in your scene!");
                else
                    eventManager.Init();
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (dictByType == null)
            dictByType = new DictionaryByType();
    }

    public static void StartListening<T>(string eventName, Action<T> listener)
    {
        Action<T> thisEvent;
        if (instance.dictByType.TryGet(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.dictByType[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.dictByType.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, Action listener)
    {
        Action thisEvent;
        if (instance.dictByType.TryGet(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.dictByType[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.dictByType.Add(eventName, thisEvent);
        }
    }

    public static void StopListening<T>(string eventName, Action<T> listener)
    {
        if (eventManager == null) return;
        Action<T> thisEvent;
        if (instance.dictByType.TryGet(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.dictByType[eventName] = thisEvent;
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (eventManager == null) return;
        Action thisEvent;
        if (instance.dictByType.TryGet(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.dictByType[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent<T>(string eventName, T eventParam)
    {
        Action<T> thisEvent = null;
        if (instance.dictByType.TryGet(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        Action thisEvent = null;
        if (instance.dictByType.TryGet(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}

public class DictionaryByType
{
    private readonly IDictionary<string, object> dictionary = new Dictionary<string, object>();

    public void Add<T>(string key, T value)
    {
        dictionary.Add(key, value);
    }

    public void Put<T>(string key, T value)
    {
        dictionary[key] = value;
    }

    public T Get<T>(string key)
    {
        return (T)dictionary[key];
    }

    public bool TryGet<T>(string key, out T value)
    {
        object tmp;
        if (dictionary.TryGetValue(key, out tmp))
        {
            value = (T)tmp;
            return true;
        }
        value = default(T);
        return false;
    }

    public object this[string str]
    {
        get { return dictionary[str]; }
        set { dictionary[str] = value; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus<TParameters> : MonoBehaviour
    where TParameters : struct
{

    [Serializable]
    public class GameEvent : UnityEvent<TParameters> { };

    private static EventBus<TParameters> _instance;
    private Dictionary<string, GameEvent> _eventDictionary;


    void Awake()
    {
        if (_instance != null)
            return;
        _instance = GetComponent<EventBus<TParameters>>();
        if (_eventDictionary == null)
            _eventDictionary = new Dictionary<string, GameEvent>();
    }

    /// <summary>Subscribe event</summary>
    /// <param name="eventName">event name</param>
    /// <param name="listener">event handler method</param>
    public void Subscribe(string eventName, UnityAction<TParameters> listener)
    {
        
            if (_instance._eventDictionary.TryGetValue(eventName, out GameEvent thisEvent))
                thisEvent.AddListener(listener);
            else
            {
                thisEvent = new GameEvent();
                thisEvent.AddListener(listener);
                _instance._eventDictionary.Add(eventName, thisEvent);
            }
            
    }
    
    /// <summary>Unsubscribe event</summary>
    /// <param name="eventName">event name</param>
    /// <param name="listener">event handler method</param>
    public void Unsubscribe(string eventName, UnityAction<TParameters> listener)
    {
        if (_instance == null)
            return;
        if (_instance._eventDictionary.TryGetValue(eventName, out GameEvent thisEvent))
            thisEvent.RemoveListener(listener);
    }

    /// <summary>Send event</summary>
    /// <param name="eventName">event name</param>
    /// <param name="parameters">arg parametrs</param>
    /// 
    
    public void SendEvent(string eventName, TParameters parameters)
    {
        if (_instance._eventDictionary.TryGetValue(eventName, out GameEvent thisEvent))
        {
            thisEvent.Invoke(parameters);
        }
    }
    //example use
    /*
    void OnEnable () {
	    EventBus.Subscribe("event_name", MyFunction);
    }

    void OnDisable () {
	    EventBus.Unsubscribe("event_name", MyFunction);
    }

    void MyFunction (object[] parameters) {
	    Debug.Log (parameters.Length); // количество параметров -> 3 в примере
	    Debug.Log (parameters[1]);        // выведет -> 1 
    }

    ...

    EventBus.SendEvent ("event_name", "param_string", 1, 2); // вызов события
    */
}

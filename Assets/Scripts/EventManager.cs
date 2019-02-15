using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, Event> eventDictionnary;

    private static EventManager _eventManager;
    
    [System.Serializable]
    public class Event : UnityEvent<int> { }

    public static EventManager instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogError("no EventManager found");
                }
                 }

    void Init()
    {
        if (eventDictionnary == null)
        {
            eventDictionnary = new Dictionary<string, Event>();
        }
    }

    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        Event thisEvent = null;
        if (instance.eventDictionnary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new Event();
            thisEvent.AddListener(listener);
            instance.eventDictionnary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (_eventManager == null)
        {
            return;
        }

        Event thisEvent = null;
        if (instance.eventDictionnary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int eventId)
    {
        Event thisEvent = null;
        if (instance.eventDictionnary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventId);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

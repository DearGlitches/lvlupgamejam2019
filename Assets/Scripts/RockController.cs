    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.Events;

    public class RockController : MonoBehaviour
    {

        private static string _EVENTNAME = "destroyRock"; 
        private UnityAction<int> _listener;
        public int eventId = -1;
    
    private void Awake()
    {
        _listener = new UnityAction<int>(SelfDestroy);
    }

    private void OnEnable()
    {
        EventManager.StartListening(_EVENTNAME, _listener);
        Debug.Log("listening...");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelfDestroy(int id)
    {
        Debug.Log("in self destroy");
        if (id != eventId)
        {
            return;
        }
        else
        {
            EventManager.StopListening( _EVENTNAME, _listener);
            Destroy(gameObject);
        }
    }
}

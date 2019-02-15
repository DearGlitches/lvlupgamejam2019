using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour {

    private UnityAction<int> _someListener;

    void Awake ()
    {
        Debug.Log("start");
        _someListener = new UnityAction<int>(SomeFunction);
    }

    void OnEnable ()
    {
        EventManager.StartListening ("test", _someListener);
        EventManager.StartListening ("Spawn", SomeOtherFunction);
        EventManager.StartListening ("Destroy", SomeThirdFunction);
    }

    void OnDisable ()
    {
        EventManager.StopListening ("test", _someListener);
        EventManager.StopListening ("Spawn", SomeOtherFunction);
        EventManager.StopListening ("Destroy", SomeThirdFunction);
    }

    void SomeFunction (int id)
    {
        Debug.Log ("Some Function was called!" + id);
    }
    
    void SomeOtherFunction (int id)
    {
        Debug.Log ("Some Other Function was called!" + id);
    }
    
    void SomeThirdFunction (int id)
    {
        Debug.Log ("Some Third Function was called!" + id);
    }
}
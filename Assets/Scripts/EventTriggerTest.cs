using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour {


    void Update () {
        if (Input.GetKeyDown ("q"))
        {
            EventManager.TriggerEvent ("test", 1);
        }

        if (Input.GetKeyDown ("o"))
        {
            EventManager.TriggerEvent ("Spawn", 2);
        }

        if (Input.GetKeyDown ("p"))
        {
            EventManager.TriggerEvent ("Destroy", 3);
        }

        if (Input.GetKeyDown ("x"))
        {
            EventManager.TriggerEvent ("Junk", 4);
        }
    }
}
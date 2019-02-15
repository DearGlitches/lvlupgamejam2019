using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightManagement : MonoBehaviour
{

    public Transform endTarget;
    public Transform player;
    public Light lightTarget;

    private float endTargetStartPosY = 0;
    public int multiplier = 8;

    // Start is called before the first frame update
    void Start()
    {
        endTargetStartPosY = endTarget.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y != 0) {
            lightTarget.intensity = (endTarget.transform.position.y - player.transform.position.y) / endTargetStartPosY * multiplier;
        }
        

    }
}

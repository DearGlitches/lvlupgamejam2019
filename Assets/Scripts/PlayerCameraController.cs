using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public GameObject Claudine;

    private Vector3 _cameraOffset;
	
    // Use this for initialization
    void Start ()
    {
        _cameraOffset = transform.position - Claudine.transform.position;
    }
	
    // Update is called once per frame
    void LateUpdate () {
        transform.position = Claudine.transform.position + _cameraOffset;
    }
}

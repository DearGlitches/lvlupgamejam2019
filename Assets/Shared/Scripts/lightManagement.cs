using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightManagement : MonoBehaviour
{

    public Transform endTarget;
    public Transform player;
    public Light lightTarget;
    public Camera camera;
    public Color color1;
    public Color color2;
    public AudioSource underwater1;
    public AudioSource underwater2;
    public AudioSource underwater3;

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
            float delta = (endTarget.transform.position.y - player.transform.position.y) / endTargetStartPosY * multiplier / 10;
            lightTarget.intensity = delta*10;
            camera.backgroundColor = Color.Lerp(color1, color2, delta);
        }
    }
}

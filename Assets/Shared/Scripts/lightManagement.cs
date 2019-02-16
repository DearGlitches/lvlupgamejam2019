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
    public Material rockMaterial;

    private float endTargetStartPosY = 0;
    public int multiplier = 8;

    // Start is called before the first frame update
    void Start()
    {
        endTargetStartPosY = endTarget.transform.position.y;

        if (underwater1)
        {
            underwater1.volume = 1;
            underwater1.Play();
        }
        if (underwater2)
        {
            underwater2.volume = 0;
            underwater2.Play();
        }
        if (underwater3)
        {
            underwater3.volume = 0;
            underwater3.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y != 0) {
            float delta = (endTarget.transform.position.y - player.transform.position.y) / endTargetStartPosY * multiplier / 10;
            float deltasound1 = (endTarget.transform.position.y - player.transform.position.y) / endTargetStartPosY * multiplier / 10;
            lightTarget.intensity = delta*10;

            if (delta > .80f)
            {
                underwater1.volume = 1;
            }
            else if (delta > .7f)
            {
                underwater1.volume = 0.6f;
                underwater2.volume = 0.3f;
            }
            else if (delta > .6f)
            {
                underwater1.volume = 0.3f;
                underwater2.volume = 0.6f;
            }
            else if (delta > .5f)
            {
                underwater1.volume = 0.0f;
                underwater2.volume = 1;
            }
            else if (delta > .3f)
            {
                underwater2.volume = 0.6f;
                underwater3.volume = 0.3f;
            }
            else if (delta > .2f)
            {
                underwater2.volume = 0.3f;
                underwater3.volume = 0.6f;
            }
            else if (delta > .1f)
            {
                underwater2.volume = 0.0f;
                underwater3.volume = 1f;
            }

            rockMaterial.SetFloat("_OcclusionStrength", delta);

            camera.backgroundColor = Color.Lerp(color1, color2, delta);
        }
    }
}

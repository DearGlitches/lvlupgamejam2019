using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        ParticleSystem ps= GetComponent<ParticleSystem>();
        Debug.Log("Particle is playing: " + ps.isPlaying);
        ps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

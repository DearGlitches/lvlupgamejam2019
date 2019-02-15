using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class PearlController : MonoBehaviour
{
    public int eventId = -2;
    private float _rayRadius = 0.3f;

    public Boolean hasCollided = false;


    public Boolean destroyRandomRocks = false;
    public int maxEventId = 1;
    private Random _random;
    
    [Header("Sound")]
     private AudioSource _audioSrc;
     [FormerlySerializedAs("attackSound")] public AudioClip triggerSound;
    [Range(0, 1)] public float volume;
    // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponents<AudioSource>()[0];
        _random = new Random();
    }



    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, _rayRadius, Vector2.up);
        if (!hasCollided && hit.collider != null && hit.collider.CompareTag("Player"))

            if (!destroyRandomRocks)
            {
                EventManager.TriggerEvent("destroyRock", eventId);
            }
            else
            {
                EventManager.TriggerEvent("destroyRock", _random.Next(maxEventId));
            }
            _audioSrc.PlayOneShot(triggerSound, volume);
            hasCollided = true;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class PearlController : MonoBehaviour
{
    public int eventId = -2;
    private float _rayRadius = 1f;

    public Boolean hasCollided;


    public Boolean destroyRandomRocks;
    public int maxEventId = 1;
    private Random _random;

    [Header("Sprites")] 
    public Sprite spriteBeforeTrigger;
    public Sprite spriteAftertTrigger;
    
    [Header("Sound")]
     private AudioSource _audioSrc;
     [FormerlySerializedAs("attackSound")] public AudioClip triggerSound;
    [Range(0, 1)] public float volume;
    // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponents<AudioSource>()[0];
        _random = new Random();
        GetComponent<SpriteRenderer>().sprite = spriteBeforeTrigger;
    }



    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, _rayRadius, Vector2.zero);
        Debug.DrawRay(gameObject.transform.position, Vector3.zero);
        if (!hasCollided && hit.collider != null && hit.collider.CompareTag("Player"))
        {

            if (!destroyRandomRocks)
            {
                EventManager.TriggerEvent("destroyRock", eventId);
            }
            else
            {
                EventManager.TriggerEvent("destroyRock", _random.Next(maxEventId));
            }
            _audioSrc.PlayOneShot(triggerSound, volume);
            GetComponent<SpriteRenderer>().sprite = spriteAftertTrigger;
            hasCollided = true;
        }
    }
}

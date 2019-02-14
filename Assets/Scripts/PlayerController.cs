using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class PlayerController : MonoBehaviour
{

    private GameManager _gameManager;

    [Header("Player Settings")]
    public float Air;
    public float Speed;

    private float _init_air;

    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;

    private GameObject _airBubble;
    private float _initAirBubbleScale;
    private float _previousAirBubbleScale;
    
    [Header("Sounds")] 
    [Range(0, 1)] public float SoundVolume;
    public AudioClip Ouch1Sound;
    public AudioClip Ouch2Sound;
    public AudioClip Die1Sound;
    public AudioClip Die2Sound;
    public AudioClip BloopSound;

    private AudioSource _ouchAudioSrc;
    private AudioSource _dieAudioSrc;
    private AudioSource _bleepAudioSrc;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManagerObject = GameObject.Find ("GameManager");
        if (gameManagerObject != null)
            _gameManager = gameManagerObject.GetComponent<GameManager> ();
        
        _collider2D = GetComponent<CapsuleCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _airBubble = GameObject.FindGameObjectWithTag("AirBubble");
        _initAirBubbleScale = _airBubble.transform.localScale.sqrMagnitude;
        _previousAirBubbleScale = _initAirBubbleScale;
        
        // Sets the rotation to point up at start
        //transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);

        _init_air = Air;

        Air = 1;
        
        // Sets gravity to zero so the rigidbody isn't pulled down
        Physics2D.gravity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovementAxis = Input.GetAxis("Horizontal");

        float verticalMovementAxis = Input.GetAxis("Vertical");
        
        
        Vector2 velocity = new Vector2(verticalMovementAxis * Speed, horizontalMovementAxis * Speed);
        _rigidbody2D.velocity = velocity;
        
        if (velocity != Vector2.zero) {
            float angle = -Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        Air -= 1.0f / _init_air;

        Air = Math.Abs(Air);

        float airBubbleScale = Air;

        _airBubble.transform.localScale = new Vector3(
            Air,
            Air,
            Air
        );
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{

    private GameManager _gameManager;

    [Header("Player Settings")]
    public float Air;
    public float Speed;

    private float _init_air;

    private GameObject _claudine;

    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;

    private GameObject _sprite;
    private GameObject _airBubble;
    
    private Animator _spriteAnimator;

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

        _claudine = this.gameObject;
        
        _collider2D = GetComponent<CapsuleCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _sprite = _claudine.transform.GetChild(0).gameObject;
        _airBubble = _claudine.transform.GetChild(1).gameObject;

        _spriteAnimator = _sprite.GetComponent<Animator>();
        
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
        
        
        Vector2 velocity = new Vector2(horizontalMovementAxis * Speed, verticalMovementAxis * Speed);
        _rigidbody2D.velocity = velocity;
        
        if (velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _spriteAnimator.speed = 1;
        }
        else
        {
            _spriteAnimator.speed = 0;
        }
       

        Air -= 1.0f / _init_air;

        if (Air < 0)
        {
            Drown();
        }

        Air = Math.Abs(Air);

        _airBubble.transform.localScale = new Vector3(
            Air,
            Air,
            Air
        );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("AirBubble"))
        {
            other.gameObject.SetActive(false);
            CollectAirBubble();
        }
    }

    void Drown()
    {
        Debug.Log("Claudine has drowned");
        _gameManager.CurrentGameState = GameManager.GameState.End;
    }

    void CollectAirBubble()
    {
        Debug.Log("Collected Air bubble");
        
    }
}

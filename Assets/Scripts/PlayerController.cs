using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    private GameManager _gameManager;

    [Header("Player Settings")]
    public float Air;
    public float Speed;
    public Light life;

    private float _init_air;

    private GameObject _claudine;

    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;

    private GameObject _sprite;
    private GameObject _airBubble;
    private Light _airBubbleLight;

    private Animator _spriteAnimator;

    private float _initAirBubbleScale;
    private float _previousAirBubbleScale;

    [Header("Current")]
    public float Force;
    
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
    private AudioSource _heartAudioSrc;

    private bool drowned;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManagerObject = GameObject.Find ("GameManager");
        if (gameManagerObject != null)
            _gameManager = gameManagerObject.GetComponent<GameManager> ();
        else
            Debug.LogError("No game manager found");

        _claudine = this.gameObject;
        
        _collider2D = GetComponent<CapsuleCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _sprite = _claudine.transform.GetChild(0).gameObject;
        _airBubble = _claudine.transform.GetChild(1).gameObject;

        _spriteAnimator = _sprite.GetComponent<Animator>();

        _ouchAudioSrc = GetComponents<AudioSource>()[0];
        _dieAudioSrc = GetComponents<AudioSource>()[1];
        _bleepAudioSrc = GetComponents<AudioSource>()[2];
        _heartAudioSrc = GetComponents<AudioSource>()[3];

        _airBubbleLight = _airBubble.transform.GetComponentInChildren<Light>();

        // Sets the rotation to point up at start
        //transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);

        _init_air = Air;
        
        // Sets gravity to zero so the rigidbody isn't pulled down
        Physics2D.gravity = Vector2.zero;

        drowned = false;

        StartCoroutine(_gameManager.FadeAndLoadScene(GameManager.FadeDirection.Out, "NO_CHANGE"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!drowned) {
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
                transform.rotation = Quaternion.identity;
            }

            Air -= 0.5f;

            if (Air < 0)
            {
                Drown();
            }
            else
            {
                //Debug.Log(Air);

                float airScale = Mathf.Abs(Air / _init_air);

                _airBubble.transform.localScale = new Vector3(
                    airScale,
                    airScale,
                    airScale
                );

                life.color = Color.Lerp(Color.red, Color.green, airScale);


                float heartVolume = Mathf.Abs(1.1f - airScale);
                if (heartVolume > 0.75f)
                {
                    _heartAudioSrc.volume = heartVolume;
                    _heartAudioSrc.pitch = heartVolume * 2 - 0.1f;

                }
                _airBubbleLight.intensity = Mathf.Exp(heartVolume + 2.0f) - 7f;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("AirBubble"))
        {
            other.gameObject.SetActive(false);
            CollectAirBubble();
        }

        if (other.gameObject.CompareTag("Urchin"))
        {
            TouchUrchin();
        }

        if (other.gameObject.CompareTag("Current"))
        {
            Current();
        }
    }

    void Drown()
    {
        drowned = true;
        Debug.Log("Claudine has drowned");
        _dieAudioSrc.PlayOneShot(Die1Sound, SoundVolume);
        _dieAudioSrc.PlayOneShot(Die2Sound, SoundVolume);
        _heartAudioSrc.volume = 0;
        _airBubbleLight.intensity = 0;
        _heartAudioSrc.Stop();
        _spriteAnimator.speed = 0;

        _gameManager.CurrentLevelState = GameManager.LevelState.Dead;
    }

    void CollectAirBubble()
    {
        Debug.Log("Collected Air bubble");
        Debug.Log(Air);
        Air += (_init_air / 10);
        _bleepAudioSrc.PlayOneShot(BloopSound, SoundVolume);
        Debug.Log(Air);
    }

    void TouchUrchin()
    {
        Debug.Log("Touched an Urchin");
        Debug.Log(Air);
        
        if (Random.Range(0, 10) > 4)
        {
            _ouchAudioSrc.PlayOneShot(Ouch1Sound, SoundVolume);
        }
        else
        {
            _ouchAudioSrc.PlayOneShot(Ouch2Sound, SoundVolume);
        }

        Air -= 100 * (1.0f / _init_air);
        Debug.Log(Air);
    }

    void Current()
    {
        Debug.Log("Applying current");
        // NOT GREAT
        _rigidbody2D.AddForce(Vector2.left * Force);
    }
    
    
}

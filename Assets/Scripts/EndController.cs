using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{

    public GameObject Reto;
    public GameObject Claudine;
    public GameObject Heart;

    private Animator _retoAnimator;
    private Animator _heartAnimator;
    private Animator _claudineAnimator;

    private float _timer;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManagerObject = GameObject.Find ("GameManager");
        if (gameManagerObject != null)
            _gameManager = gameManagerObject.GetComponent<GameManager> ();
        else
            Debug.LogError("No game manager found");
        
        _retoAnimator = Reto.GetComponent<Animator>();
        _heartAnimator = Heart.GetComponent<Animator>();
        _claudineAnimator = Claudine.GetComponent<Animator>();

        _retoAnimator.speed = 0;
        _heartAnimator.speed = 0;
        _claudineAnimator.speed = 0;

        Heart.SetActive(false);

        _timer = 0;

        _gameManager._endMusic.volume = 1f;

        _gameManager._startedCoroutine = false;
        StartCoroutine(_gameManager.FadeAndLoadScene(GameManager.FadeDirection.Out, "NO_CHANGE"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Timer: " + _timer);
        if (_timer > 1f)
        {
            _claudineAnimator.speed = 1;
            if (_timer > 2f)
            {
                _retoAnimator.speed = 1;
                if (_timer > 3f)
                {
                    Heart.SetActive(true);
                    _heartAnimator.speed = 1;
                }
            }
        }
        _timer += Time.fixedDeltaTime;
    }
}

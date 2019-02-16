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

    private int _count;
    
    // Start is called before the first frame update
    void Start()
    {
        _retoAnimator = Reto.GetComponent<Animator>();
        _heartAnimator = Heart.GetComponent<Animator>();
        _claudineAnimator = Claudine.GetComponent<Animator>();

        _retoAnimator.speed = 0;
        _heartAnimator.speed = 0;
        _claudineAnimator.speed = 0;

        Heart.SetActive(false);

        _count = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (_count < 40)
            _claudineAnimator.speed = 1;
        if (_count < 30)
            _retoAnimator.speed = 1;
        if (_count < 15)
        {
            Heart.SetActive(true);
            _heartAnimator.speed = 1;
        }

        if (_count > 0)
            _count--;
    }
}

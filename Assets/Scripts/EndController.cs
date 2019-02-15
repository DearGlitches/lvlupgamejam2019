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
    
    // Start is called before the first frame update
    void Start()
    {
        _retoAnimator = Reto.GetComponent<Animator>();
        _heartAnimator = Heart.GetComponent<Animator>();
        _claudineAnimator = Claudine.GetComponent<Animator>();

        _retoAnimator.speed = 0;
        _heartAnimator.speed = 0;
        //_claudineAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

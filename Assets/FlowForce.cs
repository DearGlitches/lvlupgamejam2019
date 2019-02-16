using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowForce : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.0f, 1.0f)] public float HitDistance;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rigidbody = other.rigidbody;
            
            AddExplosionForce(rigidbody, 0.2f, gameObject.transform.position, 3f);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        {
            Rigidbody2D rigidbody = other.rigidbody;
            
            AddExplosionForce(rigidbody, 0.1f, gameObject.transform.position, 3f);
        }
    }

    public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
    {
        Vector3 dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0) {
            calc = 0;		
        }

        body.AddForce (dir.normalized * expForce * calc);
    }
}

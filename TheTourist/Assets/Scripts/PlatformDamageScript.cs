using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDamageScript : MonoBehaviour
{

    public float damage = 8.0f;

    // repulsion force in newton
    public float repulsion_force = 8000.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // deal damage
        HealthScript health_script = collision.gameObject.GetComponent<HealthScript>();

        if(health_script != null)
        {
            health_script.takeHealth(damage);
        }

        // apply force along collision normal
        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if(rigidbody != null)
        {
            if(rigidbody.velocity.magnitude < 100.0f)
            {
                Vector3 normal = collision.GetContact(0).normal;
                rigidbody.AddForce(-normal * (repulsion_force));
            }
            
        }
    }
}

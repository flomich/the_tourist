using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public float threshold = 800.0f;
    public float boost = 800;
    // bounce angle in degrees
    public float bounce_angle = 40.0f;

    public ParticleSystem particle_system;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // get one collision contact point
        Vector3 contact_point = collision.GetContact(0).point;
        Vector3 vec_to_contact_point = 
            Vector3.Normalize(contact_point - gameObject.transform.position);

        float cos =  Mathf.Cos(bounce_angle * Mathf.Deg2Rad);
        if (Vector3.Dot(vec_to_contact_point, Vector3.up) > cos)
        {
            if(collision.rigidbody != null)
            {
                // limit forces 
                if (collision.rigidbody.velocity.magnitude < threshold)
                {
                    collision.rigidbody.AddForce(Vector3.up * collision.rigidbody.mass * boost);
                }

                if(collision.relativeVelocity.magnitude > 0.0f)
                {
                    ParticleEffectsScript.Instance.createParticleSystem(particle_system, contact_point);
                }
                    
            }
            

        }

    }
}

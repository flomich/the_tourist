using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public float threshold = 800.0f;
    public float boost = 800;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // get one collision contact point
        Vector3 contact_point = collision.GetContact(0).point;
        Vector3 vec_to_contact_point = 
            Vector3.Normalize(contact_point - gameObject.transform.position);
        if (Vector3.Dot(vec_to_contact_point, Vector3.up) > 0.8f)
        {
            if(collision.rigidbody.velocity.magnitude < threshold)
            collision.rigidbody.AddForce(Vector3.up * collision.rigidbody.mass * boost);

        }

    }
}

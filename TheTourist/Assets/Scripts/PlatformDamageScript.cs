﻿using System.Collections;
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

        if (health_script != null)
        {
            health_script.takeHealth(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if(gameObject.tag.Contains("Player") || gameObject.tag.Contains("Enemy"))
        {
            // apply force along collision normal
            Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {

                Vector3 direction = new Vector2(7.0f * Random.Range(-1.0f, 1.0f), 0.0f) *
                    repulsion_force * 0.5f +
                    -collision.GetContact(0).normal * (repulsion_force);
                rigidbody.AddForce(direction);

            }
        }

    }
}

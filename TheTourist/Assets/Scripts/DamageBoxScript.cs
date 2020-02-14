using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoxScript : MonoBehaviour
{
    public ParticleSystem particle_system;
    public float particle_offset_x = 0.0f;
    public float particle_offset_y = 1.0f;

    public float damage = 2.0f;
    public float rest_interval = 1.0f;
    public float active_interval = 2.0f;
    public bool randomize_start_time = false;
    public bool start_active = true;

    

    private float rest_interval_timer = 0.0f;
    private float active_interval_timer = 2.0f;

    private float timer = 0.0f;

    private bool active = true;
    private bool resting = false;

    private List<GameObject> objects_in_range;

    public void Start()
    {
        objects_in_range = new List<GameObject>();

        if(start_active)
        {
            if(rest_interval <= 0.0f)
            {
                // enable particle effect (looping)
                ParticleEffectsScript.Instance.createParticleSystem(particle_system, transform.position +
                    new Vector3(particle_offset_x, particle_offset_y, 0.0f), gameObject, true);
            }
            else
            {
                // enable particle effect (lifetime)
                ParticleEffectsScript.Instance.createParticleSystem(particle_system, transform.position +
                    new Vector3(particle_offset_x, particle_offset_y, 0.0f), active_interval, gameObject);
            }
            
            active = true;
            resting = false;
            active_interval_timer = active_interval;
        }
        else
        {
            active = false;
            resting = true;
            rest_interval_timer = rest_interval;
        }

        if(randomize_start_time)
        {
            active_interval_timer += active_interval_timer * Random.Range(0.0f, 0.5f);
            rest_interval_timer += rest_interval_timer * Random.Range(0.0f, 0.5f);
        }
    }


    public void Update()
    {
        // decrement timers
        rest_interval_timer -= Time.deltaTime;
        active_interval_timer -= Time.deltaTime;

        if(resting && rest_interval_timer <= 0.0f)
        {
            // swap states
            active_interval_timer = active_interval;
            active = true;
            resting = false;

            // enable particle effect
            ParticleEffectsScript.Instance.createParticleSystem(particle_system, transform.position +
                new Vector3(particle_offset_x, particle_offset_y, 0.0f), active_interval, gameObject);

            Debug.Log("Resting timer ran out");
        }
        if(active)
        {
            Debug.Log("Active");
            if (active_interval_timer > 0.0f)
            {
                Debug.Log("Stay Active");
                timer -= Time.deltaTime;

                if(timer <= 0.0f)
                {
                    // deal damage
                    foreach (GameObject o in objects_in_range)
                    {
                        HealthScript health_script = o.GetComponent<HealthScript>();

                        if (health_script != null)
                        {
                            // apply damage to everything with a health script
                            health_script.takeHealth(damage);
                        }
                    }

                    timer = 0.5f;
                }

            }
            else if (rest_interval > 0.0f)
            {
                Debug.Log("Resting");
                // swap states
                rest_interval_timer = rest_interval;
                active = false;
                resting = true;
            }
            else
            {
                active_interval_timer = active_interval;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // only store references for objects with health

        HealthScript health_script = collision.gameObject.GetComponent<HealthScript>();

        if(health_script != null)
        {
            objects_in_range.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(objects_in_range.Contains(collision.gameObject))
        {
            objects_in_range.Remove(collision.gameObject);
        }

    }

}

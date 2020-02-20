using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDamageScript : MonoBehaviour
{

    public float damage = 8.0f;

    // repulsion force in newton
    public float repulsion_force = 8000.0f;

    List<GameObject> objects_in_range;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        objects_in_range = new List<GameObject>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            foreach(GameObject o in objects_in_range)
            {
                // deal damage
                HealthScript health_script = o.GetComponent<HealthScript>();

                if (health_script != null)
                {
                    health_script.takeHealth(damage);
                }
            }

            timer = 0.5f;
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!objects_in_range.Contains(collider.gameObject))
        {
            objects_in_range.Add(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (objects_in_range.Contains(collider.gameObject))
        {
            objects_in_range.Remove(collider.gameObject);
        }
    }


   /*private void OnCollisionStay2D(Collision2D collision)
    {

        if(collision.gameObject.tag.Contains("Player") || collision.gameObject.tag.Contains("Enemy"))
        {

            // apply force along collision normal
            Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {

                Vector3 direction = new Vector2(0.01f * Random.Range(-1.0f, 1.0f), 0.25f * Random.Range(-1.0f, 1.0f)) *
                    repulsion_force * 0.5f +
                    -collision.GetContact(0).normal;
                rigidbody.AddForce(direction.normalized * (repulsion_force));

            }
        }

    }*/
}

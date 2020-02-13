using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    //the animator of the player
    public Animator animator;

    // punch force in newton
    public float punch_force = 250.0f;
    public float punch_cooldown = 2.0f;
    public float punch_damage = 20.0f;

    private bool punch_state = false;
    private float punch_timer = 0.0f;

    private float double_damage_timer = 0.0f;
    private bool has_double_damage = false;


    private List<GameObject> objects_in_range;

    // Start is called before the first frame update
    void Start()
    {
        objects_in_range = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        punch_timer -= Time.deltaTime;
        double_damage_timer -= Time.deltaTime;

        if(double_damage_timer <= 0.0f)
        {
            has_double_damage = false;
        }

        if (punch_state && punch_timer < 0.0f)
        {
            punch();
            punch_timer = punch_cooldown;
        }

        if (punch_timer < 0.9)
            animator.SetInteger("PunchState", 0);
    }

    public void activateDoubleDamage(float seconds)
    {
        has_double_damage = true;
        double_damage_timer = seconds;
    }

    private void punch()
    {
        // play punch sound
        SoundEffectScript.Instance.playPunchSound(gameObject.transform.position);

        // animate punch
        animator.SetInteger("PunchState", 1);

        // get player forward vector from scale
        Vector3 forward_vector;
        if (gameObject.transform.localScale.x < 0.0f)
        {
            forward_vector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            forward_vector = new Vector3(-1.0f, 0.0f, 0.0f);
        }


        foreach (GameObject o in objects_in_range)
        {
            // dont apply damage or force to self
            if (gameObject.Equals(o)) continue;

            // only apply forces and damage to objects in front of player
            Vector3 vec_to_other = (o.transform.position - gameObject.transform.position);

            if(Vector3.Dot(vec_to_other.normalized, forward_vector) < 0.0f && vec_to_other.magnitude > 0.5)
            {
                // other object is not in front of player
                continue;
            }

            // deal damage
            HealthScript health_script = o.GetComponent<HealthScript>();
            if(health_script != null)
            {
                if (has_double_damage)
                    health_script.takeHealth(punch_damage * 2.0f);
                else
                    health_script.takeHealth(punch_damage);
            }

            // add force
            Rigidbody2D rigid_body = o.GetComponent<Rigidbody2D>();
            if(rigid_body != null)
            {
                rigid_body.AddForce(forward_vector * punch_force);
            }
        }

    }

    public void setPunchState(bool state)
    {
        punch_state = state;
    }

    public void addObjectInRange(GameObject game_object)
    {
        objects_in_range.Add(game_object);
    }

    public void removeObjectInRange(GameObject game_object)
    {
        if (objects_in_range.Contains(game_object) )
        {
            objects_in_range.Remove(game_object);
        }
    }
}

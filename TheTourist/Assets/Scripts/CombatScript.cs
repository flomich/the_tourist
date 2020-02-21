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
    public float animation_timer = 0.1f;

    private float double_damage_timer = 0.0f;
    private bool has_double_damage = false;

    private float double_speed_timer = 0.0f;
    private bool has_double_speed = false;

    public ParticleSystem punch_particle_system;
    private List<GameObject> objects_in_range;

    private float animation_speed = 4.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        objects_in_range = new List<GameObject>();
        punch_state = false;

        // clamp punch cooldown to min 0.1 sec
        punch_cooldown = Mathf.Max(0.1f, punch_cooldown);

        if(tag.Contains("Enemy"))
        {
            punch_timer = punch_cooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // decrement timers
        punch_timer -= Time.deltaTime;
        double_damage_timer -= Time.deltaTime;
        animation_timer -= Time.deltaTime;
        double_speed_timer -= Time.deltaTime;

        if (double_damage_timer <= 0.0f)
        {
            has_double_damage = false;
        }

        if(double_speed_timer <= 0.0f)
        {
            has_double_speed = false;
        }

        if (punch_state && punch_timer < 0.0f)
        {
            punch();

            if(has_double_speed)
            {
                punch_timer = punch_cooldown * 0.5f;
            }
            else
            {
                punch_timer = punch_cooldown;
            }

            animation_timer = 0.1f;
        }

        if (animation_timer < 0.0f)
            animator.SetInteger("PunchState", 0);
    }

    public void activateDoubleDamage(float seconds)
    {
        has_double_damage = true;
        double_damage_timer = seconds;
    }

    public void activateDoubleSpeed(float seconds)
    {
        has_double_speed = true;
        double_speed_timer = seconds;
    }

    private void playPunchEffects(Vector3 forward_vector)
    {
        // play punch sound
        SoundEffectScript.Instance.playPunchSound(gameObject.transform.position);

        //Fetch the Collider from the GameObject
        Collider2D collider = GetComponent<Collider2D>();

        //Fetch the center of the Collider volume
        Vector3 center = collider.bounds.center;
        Vector3 position = center + forward_vector;
        ParticleEffectsScript.Instance.createParticleSystem(punch_particle_system, position, gameObject);

    }

    private void punch()
    {
        // animate punch
        animator.SetInteger("PunchState", 1);
        
        if(has_double_speed)
        {
            animator.speed = animation_speed * 2.5f;
        }
        else
        {
            animator.speed = animation_speed;
        }
        

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

        bool hit = false;
        foreach (GameObject o in objects_in_range)
        {
            // dont apply damage or force to self
            if (gameObject.Equals(o)) continue;

            // only apply forces and damage to objects in front of player
            Vector3 vec_to_other = (o.transform.position - gameObject.transform.position);

            // get intersection distance with collider
            float distance_to_other = Mathf.Infinity;
            Collider2D collider = o.GetComponent<Collider2D>();

            Vector3 closest_point = transform.position;
            if (collider != null)
            {
                closest_point = collider.bounds.ClosestPoint(gameObject.transform.position);
                distance_to_other = Mathf.Max(Vector3.Distance(closest_point,
                                    gameObject.transform.position), 0.0f);

                if ((Vector3.Dot(vec_to_other.normalized, forward_vector) < 0.0f &&
                    distance_to_other > 0.1) || distance_to_other > 1.1f )
                {
                    // other object is not in front of player
                    continue;
                }
            }

            // deal damage
            HealthScript health_script = o.GetComponent<HealthScript>();
            if(health_script != null)
            {
                if (has_double_damage)
                    health_script.takeHealth(punch_damage * 2.0f);
                else
                    health_script.takeHealth(punch_damage);

                // stun if enemy
                if(o.tag.Contains("Enemy"))
                {
                    EnemyCommander commander = o.GetComponent<EnemyCommander>();

                    if(commander != null)
                    {
                        commander.stun();
                    }
                }
            }

            // add force
            Rigidbody2D rigid_body = o.GetComponent<Rigidbody2D>();
            if(rigid_body != null)
            {
                // Display punch sprite
                playPunchEffects(forward_vector);
                hit = true;
                Vector3 direction = (forward_vector + new Vector3(0.0f, Random.Range(0.5f, 0.8f), 0.0f)).normalized;

                if (has_double_damage)
                    rigid_body.AddForce(direction * punch_force * 2.0f);
                else
                    rigid_body.AddForce(direction * punch_force);
            }

        }

        if(!hit)
        {
            SoundEffectScript.Instance.playSwooshSound(transform.position);
        }

    }

    public void setPunchState(bool state)
    {
        punch_state = state;
    }

    public void addObjectInRange(GameObject game_object)
    {
        if(!objects_in_range.Contains(game_object))
        {
            objects_in_range.Add(game_object);
        }
        
    }

    public void removeObjectInRange(GameObject game_object)
    {
        if (objects_in_range.Contains(game_object) )
        {
            objects_in_range.Remove(game_object);
        }
    }
}

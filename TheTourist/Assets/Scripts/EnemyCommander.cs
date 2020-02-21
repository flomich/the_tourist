using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : MonoBehaviour
{
    public WayBoxScript left_way = null;
    public WayBoxScript right_way = null;
    public WayBoxScript left_wall = null;
    public WayBoxScript right_wall = null;

    public float max_stand_time = 5.0f;
    public float min_stand_time = 1.0f;

    public float max_move_time = 5.0f;
    public float min_move_time = 2.0f;

    public float move_speed = 0.25f;

    private movePlayer move_script = null;
    private HealthScript health_script = null;
    private CombatScript combat_script = null;
    private GameObject target = null;

    private float move_direction = -1.0f;
    private float stand_timer = 1.0f;
    private float move_timer = 0.0f;

    private bool stunned = false;
    private float stun_timer = 0.0f;
    public float stun_time = 1.0f;

    public GameObject stun_icon;

    void Start()
    {
        move_script = GetComponent<movePlayer>();
        health_script = GetComponent<HealthScript>();
        combat_script = GetComponent<CombatScript>();
    }

    
    void disable()
    {
        // dead enemies fall down and just lie there for now
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        // unfreeze rotation so that character falls down
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = false;
        }

        // disable movement
        move_script.animator.SetInteger("WalkState", 0);
        move_script.setMoveInput(0.0f);

        // disable combat
        combat_script.animator.SetInteger("PunchState", 0);
        combat_script.setPunchState(false);
    }

    private void idle()
    {
        WayBoxScript lway = left_way;
        WayBoxScript rway = right_way;
        WayBoxScript lwall = left_wall;
        WayBoxScript rwall = right_wall;

        if (gameObject.transform.localScale.x > 0.0f)
        {
            lway = right_way;
            rway = left_way;
            lwall = right_wall;
            rwall = left_wall;
        }

        if (stand_timer > 0.0f)
        {
            stand_timer -= Time.deltaTime;
            move_script.setMoveInput(0.0f);

            if (stand_timer < 0.0f)
            {
                move_timer = Random.Range(min_move_time, max_move_time);
            }

            return;
        }

        if (move_timer > 0.0f)
        {
            move_timer -= Time.deltaTime;

            //moving left or right?
            if (move_direction > 0.0f)
            {
                //is there something we can walk on?
                if (rway.isBlocked() && !rwall.isBlocked())
                {
                    move_script.setMoveInput(move_direction * move_speed);
                }
                else
                {
                    stand_timer = Random.Range(min_stand_time, max_stand_time);
                    move_direction *= -1.0f;
                }
            }
            else
            {
                //is there something we can walk on?
                if (lway.isBlocked() && !lwall.isBlocked())
                {
                    move_script.setMoveInput(move_direction * move_speed);
                }
                else
                {
                    stand_timer = Random.Range(min_stand_time, max_stand_time);
                    move_direction *= -1.0f;
                }
            }

            if(move_timer < 0.0f)
            {
                stand_timer = Random.Range(min_stand_time, max_stand_time);
                move_direction = Random.Range(-1.0f, 1.0f) < 0.0f ? -1.0f : 1.0f;
            }
        }

        //float move = Mathf.Sin(Time.time * 0.75f);

        //if (Mathf.Abs(move) > 0.5f)
        //{
        //    move_script.setMoveInput(move * 0.5f);
        //}
        //else
        //{
        //    move_script.setMoveInput(0.0f);
        //}
    }

    private void followPlayer(float speed_scale)
    {
        Vector3 target_pos = target.transform.position;
        Vector3 vec_to_target = target_pos - transform.position;

        // get forward vector from scale
        Vector3 forward_vector;
        if (gameObject.transform.localScale.x < 0.0f)
        {
            forward_vector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            forward_vector = new Vector3(-1.0f, 0.0f, 0.0f);
        }

        if(Vector3.Dot(forward_vector, vec_to_target) > 0.0f)
        {
            move_script.setMoveInput(forward_vector.x * speed_scale);
        }
        else
        {
            move_script.setMoveInput(-forward_vector.x * speed_scale);
        }
    }

    private void punchTarget()
    {
        if (Random.Range(0.0f, 1.0f) > 0.3f)
        {
            Vector3 target_pos = target.transform.position;
            Vector3 vec_to_target = target_pos - transform.position;

            // get forward vector from scale
            Vector3 forward_vector;
            if (gameObject.transform.localScale.x < 0.0f)
            {
                forward_vector = new Vector3(1.0f, 0.0f, 0.0f);
            }
            else
            {
                forward_vector = new Vector3(-1.0f, 0.0f, 0.0f);
            }
            // facing target?
            if (Vector3.Dot(forward_vector, vec_to_target) > 0.0f)
            {
                combat_script.setPunchState(true);
            }
                
        }
    }


    void Update()
    {

        if (health_script.health <= 0.0f)
        {
            disable();
            return;
        }

        if(stunned)
        {
            stun_timer -= Time.deltaTime;

            // still stunned?
            if(stun_timer <= 0.0f)
            {
                stunned = false;
            }

            // disable movement
            move_script.animator.SetInteger("WalkState", 0);
            move_script.setMoveInput(0.0f);

            // disable combat
            combat_script.animator.SetInteger("PunchState", 0);
            combat_script.setPunchState(false);
            return;
        }

        if(target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 1.0f)
            {
                // only follow player a little
                followPlayer(0.1f);
                punchTarget();

                return;
            }
            // follow and attack
            combat_script.setPunchState(false);
            followPlayer(0.5f);
            return;
        }
        combat_script.setPunchState(false);
        // idle if there is no target
        idle();

       
    }

    private void OnTriggerEnter2D(Collider2D other_collider)
    {
        if(other_collider.gameObject.tag == "Player")
        {
            target = other_collider.gameObject;
            combat_script.addObjectInRange(other_collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other_collider)
    {
        if (other_collider.gameObject.tag == "Player")
        {
            target = null;
            combat_script.removeObjectInRange(other_collider.gameObject);
        }
    }

    public void stun()
    {
        if(!stunned)
        {
            // Instantiate damage icon
            Quaternion rotation = Quaternion.identity;
            rotation.z = Random.Range(0.0f, 0.7f);
            Vector3 position = gameObject.transform.position + new Vector3(0.0f, 2.0f, 0.0f);

            CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();

            if (collider != null)
            {
                position.y += 1.0f;
            }
            // spwan damage icon
            GameObject icon = Instantiate(stun_icon, position, rotation);
            icon.transform.SetParent(gameObject.transform);
        }
        else
        {
            DisplayScript display_script = stun_icon.GetComponent<DisplayScript>();

            if(display_script != null)
            {
                display_script.addDisplayTime(stun_time);
            }
        }
        stunned = true;
        stun_timer = stun_time;
    }
}

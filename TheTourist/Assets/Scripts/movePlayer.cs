using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    //the animator of the player
    public Animator animator;

    //the force that is applied when moving the player
    public float move_force = 50.0f;

    //the force at which the player slows down when not moving
    public float move_slowdown_force = 8.0f;

    //the max speed the player can have while moving
    public float move_max_velocity = 5.0f;

    //a stride to smoothly stop moving when approaching max speed
    public float move_max_velocity_stride = 1.0f;

    //the force which is applied to move the player up
    public float jumping_force = 50.0f;

    //the time to apply jump force during one jump
    public float jumping_max_time = 0.1f;

    //the rigidbody of the player
    private Rigidbody2D rigidbody_2d = null;

    //the current jump time counter
    private float current_jump_time = 0.0f;

    //flag that indicates if the player can jump (cleared on ground collision)
    private bool jump_cooldown = false;

    void Start()
    {
        rigidbody_2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //accumulates movement forces (walk, jump)
        Vector2 current_movement_force = new Vector2(0.0f, 0.0f);

        //get the horizontal input
        float moveHorizontal = Input.GetAxis("Horizontal");

        //get the vertical input
        float moveVertical = Input.GetAxis("Vertical");

        //compute direction (only horizontal input)
        Vector3 direction = new Vector3(moveHorizontal, 0.0f, 0.0f);

        //flip player based on horizontal input
        transform.localScale = new Vector3(moveHorizontal < 0.0f ? 0.4f : -0.4f, 0.4f, 0.4f);

        //if there is movement input
        if (direction.magnitude < 0.0001f)
        {
            //slow down the horizontal velocity
            current_movement_force = new Vector2(-rigidbody_2d.velocity.x * move_slowdown_force, 0.0f);

            //set the animation state
            animator.SetInteger("WalkState", 0);
        }
        else
        {
            //normalize the direction
            direction.Normalize();

            //compute scaling value to cancel out force on max speed
            float x = Mathf.Clamp(rigidbody_2d.velocity.magnitude - move_max_velocity, 0.0f, move_max_velocity_stride);
            x /= move_max_velocity_stride;
            x = -(x * x) + 1.0f;

            //add jump force
            current_movement_force = direction * move_force * x;

            //set the animation state
            animator.SetInteger("WalkState", (int)direction.x);
        }

        //jumping
        if (Input.GetKey(KeyCode.Space) && !jump_cooldown)
        {
            //increment the jump time
            current_jump_time += Time.deltaTime;

            //add jump force or stop jumping if max jump time is reached
            if (current_jump_time >= jumping_max_time){
                jump_cooldown = true;
            }
            else{
                current_movement_force.y += jumping_force;
            }
        }

        //add the force to the physics object
        rigidbody_2d.AddForce(current_movement_force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //only enable jumping if contact with ground
        for(int i = 0; i < collision.contactCount; i++)
        {
            //get contact point normal
            Vector2 normal = collision.GetContact(i).normal;

            //enable jumping only if normal is facing up
            if (Vector2.Dot(normal, new Vector2(0.0f, 1.0f)) > 0.8f)
            {
                jump_cooldown = false;
                current_jump_time = 0.0f;
            }
        }
    }
}

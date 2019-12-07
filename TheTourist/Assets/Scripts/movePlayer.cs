﻿using System.Collections;
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

    //scales the movement force when in air
    public float move_air_scale = 0.25f;

    //the force which is applied to move the player up
    public float jumping_force = 50.0f;

    //the time to apply jump force during one jump
    public float jumping_max_time = 0.1f;

    //scales the animation with the walk speed
    public float animation_speed_scale = 0.3f;

    //the rigidbody of the player
    private Rigidbody2D rigidbody_2d = null;

    //the current jump time counter
    private float current_jump_time = 0.0f;

    //flag that indicates if the player can jump (cleared on ground collision)
    private bool jump_cooldown = false;

    //flag that indicates if jump is pressed
    private bool jump_button_down = false;

    private float boost_time = 0.0f;

    void Start()
    {
        rigidbody_2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //decrease the boost time
        boost_time = Mathf.Max(boost_time - Time.deltaTime, 0.0f);
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
        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            transform.localScale = new Vector3(moveHorizontal < 0.0f ? 0.4f : -0.4f, 0.4f, 0.4f);
        }

        //if there is movement input
        if (direction.magnitude < 0.0001f)
        {
            //slow down the horizontal velocity
            current_movement_force = new Vector2(-rigidbody_2d.velocity.x * move_slowdown_force, 0.0f);

            //set the animation state
            animator.SetInteger("WalkState", 0);
            animator.speed = 1.0f;
        }
        else
        {
            //normalize the direction
            direction.Normalize();

            //compute boost scale
            float boost_scale = boost_time > 0.001f ? 1.5f : 1.0f;

            //compute scaling value to cancel out force on max speed
            float x = Mathf.Clamp(rigidbody_2d.velocity.magnitude - move_max_velocity * boost_scale, 0.0f, move_max_velocity_stride);
            x /= move_max_velocity_stride;
            x = -(x * x) + 1.0f;

            //slow down movement when in air
            x = current_jump_time > 0.01f ? x * move_air_scale : x;

            //add movement force
            current_movement_force = direction * move_force * x;

            //set the animation state
            animator.SetInteger("WalkState", (int)direction.x);
            animator.speed = 0.1f + Mathf.Abs(rigidbody_2d.velocity.x) * animation_speed_scale;
        }

        //jumping
        if (Input.GetKey(KeyCode.Space) && !jump_cooldown && !jump_button_down)
        {
            //increment the jump time
            current_jump_time += Time.deltaTime;

            //add jump force or stop jumping if max jump time is reached
            if (current_jump_time >= jumping_max_time){
                jump_cooldown = true;
                jump_button_down = true;
            }
            else{
                current_movement_force.y += jumping_force;
            }
        }
        
        if(!Input.GetKey(KeyCode.Space))
        {
            jump_button_down = false;
        }

        //play jump animation (to be added)
        if (current_jump_time > 0.01f)
        {
            animator.speed = 1.0f;
        }

        //add the force to the physics object
        rigidbody_2d.AddForce(current_movement_force);
    }

    public void activateBoost(float time)
    {
        boost_time = Mathf.Clamp(time, 0.0f, 1000.0f);
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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCommander : MonoBehaviour
{
    private movePlayer move_script = null;
    private CombatScript combat_script = null;
    private InventoryScript inventory_script = null;
    private GrabScript grab_script = null;

    // Start is called before the first frame update
    void Start()
    {
        move_script = GetComponent<movePlayer>();
        combat_script = GetComponent<CombatScript>();
        inventory_script = GetComponent<InventoryScript>();
        grab_script = GetComponent<GrabScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the horizontal value of the controller stick (should be in -1, +1 range)
        float horizontal_axis = Input.GetAxis("Horizontal");

        //get the space key state
        bool space_state = Input.GetKey(KeyCode.Space);

        // get key state for punch
        bool punch_state = Input.GetKeyDown(KeyCode.Q);

        // get key state for item doener
        bool consume_doener_state = Input.GetKeyDown(KeyCode.Alpha1);

        // get key state for item doener
        bool consume_puntigamer_state = Input.GetKeyDown(KeyCode.Alpha2);

        // get key state for item doener
        bool consume_frankfurter_state = Input.GetKeyDown(KeyCode.Alpha3);

        // get grab key state
        bool grab_state = Input.GetKey(KeyCode.R);

        //set the move script input
        move_script.setMoveInput(horizontal_axis * (grab_state ? 0.5f : 1.0f));
        move_script.setJumpInput(space_state);

        // set the combat script input
        combat_script.setPunchState(punch_state);

        // set input for grab script
        grab_script.setGrabState(grab_state);

        // set inventory script input
        inventory_script.setConsumeDoener(consume_doener_state);
        inventory_script.setConsumePuntigamer(consume_puntigamer_state);
        inventory_script.setConsumeFrankfurter(consume_frankfurter_state);

        //reload scene or jump to menu on backspace
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //get current scene name
            string current_scene = SceneManager.GetActiveScene().name;

            //jump to menu if in train statiion or tutorial
            if(current_scene.Equals("TrainStation") || current_scene.Equals("Tutorial"))
            {
                //load menu scene
                SceneManager.LoadScene("Menu");
            }
            else
            {
                //reload current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other_collider)
    {

        Rigidbody2D other_rigid_body = other_collider.gameObject.GetComponent<Rigidbody2D>();

        PlatformAnimator platform_animator = other_collider.gameObject.GetComponent<PlatformAnimator>();

        // add objects that overlap the hitbox to objects in range
        if (other_rigid_body != null && platform_animator == null)
        {
            combat_script.addObjectInRange(other_collider.gameObject);

            // add to grab objects in range
            grab_script.AddObjectInRange(other_collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other_collider)
    {
        // object left the hitbox, remove from objects in range
        combat_script.removeObjectInRange(other_collider.gameObject);
        grab_script.removeObjectInRange(other_collider.gameObject);

    }
}

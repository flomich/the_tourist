using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommander : MonoBehaviour
{
    private movePlayer move_script = null;
    private CombatScript combat_script = null;

    // Start is called before the first frame update
    void Start()
    {
        move_script = GetComponent<movePlayer>();
        combat_script = GetComponent<CombatScript>();
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

        //set the move script input
        move_script.setMoveInput(horizontal_axis);
        move_script.setJumpInput(space_state);

        // set the combat script input
        combat_script.setPunchState(punch_state);

    }

    private void OnTriggerEnter2D(Collider2D other_collider)
    {
        Rigidbody2D other_rigid_body = other_collider.gameObject.GetComponent<Rigidbody2D>();

        if (other_rigid_body != null)
        {
            combat_script.addObjectInRange(other_collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other_collider)
    {

        combat_script.removeObjectInRange(other_collider.gameObject);

    }
}

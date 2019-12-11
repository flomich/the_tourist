using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommander : MonoBehaviour
{
    private movePlayer move_script = null;

    // Start is called before the first frame update
    void Start()
    {
        move_script = GetComponent<movePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the horizontal value of the controller stick (should be in -1, +1 range)
        float horizontal_axis = Input.GetAxis("Horizontal");

        //get the space key state
        bool space_state = Input.GetKey(KeyCode.Space);

        //set the move script input
        move_script.setMoveInput(horizontal_axis);
        move_script.setJumpInput(space_state);
    }
}

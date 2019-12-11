using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : MonoBehaviour
{
    private movePlayer move_script = null;

    void Start()
    {
        move_script = GetComponent<movePlayer>();
    }

    void Update()
    {
        float move = Mathf.Sin(Time.time * 0.75f);

        if(Mathf.Abs(move) > 0.5f)
        {
            move_script.setMoveInput(move * 0.5f);
        }
        else
        {
            move_script.setMoveInput(0.0f);
        } 
    }
}

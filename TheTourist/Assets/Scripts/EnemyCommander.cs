using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : MonoBehaviour
{
    private movePlayer move_script = null;
    private HealthScript health_script = null;

    void Start()
    {
        move_script = GetComponent<movePlayer>();
        health_script = GetComponent<HealthScript>();
    }

    void Update()
    {
        if (health_script.health <= 0.0f)
        {
            // dead enemies fall down and just lie there for now
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            
            // unfreeze rotation so that character falls down
            if(rigidbody != null)
            {
                Debug.Log("Unfreeze rotation");
                rigidbody.freezeRotation = false;
            }
        }
        else
        {
            float move = Mathf.Sin(Time.time * 0.75f);

            if (Mathf.Abs(move) > 0.5f)
            {
                move_script.setMoveInput(move * 0.5f);
            }
            else
            {
                move_script.setMoveInput(0.0f);
            }
        }
       
    }
}

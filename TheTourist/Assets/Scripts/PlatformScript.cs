using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformScript : MonoBehaviour
{

    private PlatformEffector2D effector;
    private float timer = 0.2f;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            timer = 0.2f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (timer <= 0.0f)
            {
                effector.rotationalOffset = 180.0f;
                timer = 0.2f;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if (Input.anyKey && !Input.GetKey(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 0.0f;
        }
    }

}

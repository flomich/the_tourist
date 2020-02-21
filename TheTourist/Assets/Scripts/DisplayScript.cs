using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    public float display_time = 20.0f;
    private float display_timer = 1000.0f;

    SpriteRenderer sprite_renderer = null;

    // Start is called before the first frame update
    void Start()
    {
        display_timer = display_time;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        display_timer -= Time.deltaTime;

        if(display_timer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void addDisplayTime(float inc)
    {
        display_timer = inc;
        display_time = inc;
    }
}

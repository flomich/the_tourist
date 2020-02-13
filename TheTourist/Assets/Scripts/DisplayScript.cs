using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    public float display_time = 20.0f;
    private float display_timer;

    SpriteRenderer sprite_renderer = null;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, display_time);
        display_timer = display_time;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    public float display_time = 2.0f;
    private float display_timer = 2.0f;

    SpriteRenderer sprite_renderer = null;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, display_timer);

        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        display_timer -= Time.deltaTime;
        float alpha = sprite_renderer.material.color.a;

        

        float faded_alpha;
        if (display_timer > display_timer * 0.5f)
        {
            float f = 1.0f - (display_timer * 0.5f) / (display_time * 0.5f);
            faded_alpha = Mathf.Lerp(0.0f, alpha, f);
        }
        else
        {
            float f = 1.0f - display_timer / (display_time * 0.5f);
            faded_alpha = Mathf.Lerp(alpha, 0.0f, f);
        }

        sprite_renderer.material.color = new Color(sprite_renderer.material.color.r, 
                                            sprite_renderer.material.color.g, 
                                            sprite_renderer.material.color.b,
                                            faded_alpha);
    }
}

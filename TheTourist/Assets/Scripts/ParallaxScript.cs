using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    //the game object
    public GameObject reference = null;

    //the strength of the parallax effect
    public float parallax_strength = 0.0f;

    //the offset in x direction
    public float x_offset = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make sure that there was a reference set
        if (!reference) return;

        transform.position = new Vector3(reference.transform.position.x * (1.0f - parallax_strength) + x_offset, transform.position.y, transform.position.z);
    }
}

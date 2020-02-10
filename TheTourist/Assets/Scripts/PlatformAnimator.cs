using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
    public float x_frequency = 1.0f;
    public float y_frequency = 1.0f;
    public float x_phase = 0.0f;
    public float y_phase = 0.0f;
    public float x_scale = 1.0f;
    public float y_scale = 1.0f;

    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = 1.0f;
        float y = 1.0f;
        float t = Time.time;

        Vector3 position = origin + 
            new Vector3(Mathf.Sin(x_frequency * t + x_phase * Mathf.PI) * x_scale, Mathf.Sin(y_frequency * t + y_phase * Mathf.PI) * y_scale, 0.0f);

        transform.position = position;
    }
}

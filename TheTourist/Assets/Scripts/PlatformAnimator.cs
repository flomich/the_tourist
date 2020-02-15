using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
    private Rigidbody2D rigid_body;

    public float x_frequency = 1.0f;
    public float y_frequency = 1.0f;
    public float x_phase = 0.0f;
    public float y_phase = 0.0f;
    public float x_scale = 1.0f;
    public float y_scale = 1.0f;

    public float stiffness = 5000.0f;
    public float damping = 10.0f;
    public float power = 2.0f;
    public float max_force = 100000.0f;

    private Vector2 origin;
    private float fixed_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;

        fixed_time = 0.0f;

        rigid_body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        fixed_time += Time.fixedDeltaTime;

        float t = fixed_time;

        Vector2 target = origin +
            new Vector2(Mathf.Sin(x_frequency * t + x_phase * Mathf.PI) * x_scale, Mathf.Sin(y_frequency * t + y_phase * Mathf.PI) * y_scale);

        Vector2 rigid_position = new Vector2(rigid_body.position.x, rigid_body.position.y);

        Vector2 force = new Vector2();

        force = getSpringForce(target, rigid_position);

        rigid_body.AddForce(force);

        rigid_body.AddForce(-rigid_body.velocity * damping);
    }

    private Vector2 getSpringForce(Vector2 target, Vector2 origin)
    {
        Vector2 vec2target = new Vector2();

        vec2target = target - origin;

        vec2target = vec2target * Mathf.Pow(vec2target.magnitude, power) * stiffness;

        if(vec2target.magnitude > max_force)
        {
            vec2target = vec2target.normalized * max_force;
        }

        return vec2target;
    }
}

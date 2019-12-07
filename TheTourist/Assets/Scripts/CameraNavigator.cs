using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNavigator : MonoBehaviour
{
    public GameObject target;
    public float y_offset = 3.0f;

    public Vector3 camera_position = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 camera_velocity = new Vector3(0.0f, 0.0f, 0.0f);
    public float follow_strength = 10.0f;
    public float follow_drag = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_position = new Vector3(0.0f, y_offset, 0.0f);
        target_position += target.transform.position;

        Vector3 v = target_position - transform.position;

        camera_velocity += v * follow_strength * Time.deltaTime;

        camera_velocity -= camera_velocity * follow_drag * Time.deltaTime;

        camera_position += camera_velocity * Time.deltaTime;

        transform.position = new Vector3(camera_position.x, camera_position.y, -10.0f);
    }
}

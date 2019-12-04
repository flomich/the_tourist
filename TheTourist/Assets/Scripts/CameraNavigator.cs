using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNavigator : MonoBehaviour
{
    public GameObject target;
    public float y_offset = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + y_offset, -10.0f);
    }
}

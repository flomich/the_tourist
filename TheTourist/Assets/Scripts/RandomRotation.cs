using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float maxRotation = 10.0f;
    void Start()
    {
        float rotZ = Random.Range(-maxRotation, maxRotation);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}

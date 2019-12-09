using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelfiePose : MonoBehaviour
{
    public GameObject player;
    public GameObject frame;

    public float tiltZ = 24.0f;
    public float width = 3.4f;
    public float posY = -4.0f;
    public float offsetCenter = 0.1f;

    public void GeneratePose() {
        float orient = Mathf.Sign(Random.Range(-1, 1));
        float posX = Random.Range(offsetCenter, 1.0f) * orient;
        float maxTilt = Mathf.Pow(posX, 2) * tiltZ;
        Vector3 rotation = new Vector3(0, orient > 0 ? 0 : 180, Random.Range(0, maxTilt));
        Vector3 pos = new Vector3(posX * width / 2, posY, 0);
        player.transform.localPosition = pos;
        player.transform.rotation = Quaternion.Euler(rotation);
    }
}

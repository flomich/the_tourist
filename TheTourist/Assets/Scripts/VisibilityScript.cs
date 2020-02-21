using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityScript : MonoBehaviour
{
    public List<GameObject> game_objects;

    private void Start()
    {
        foreach (GameObject g in game_objects)
        {
            g.SetActive(false);
            //Debug.Log("Disable");
        }
    }

    private void OnBecameVisible()
    {
        foreach(GameObject g in game_objects)
        {
            g.SetActive(true);
            //Debug.Log("Enable");
        }
    }

    private void OnBecameInvisible()
    {
        foreach (GameObject g in game_objects)
        {
            g.SetActive(false);
            //Debug.Log("Disable");
        }
    }
}

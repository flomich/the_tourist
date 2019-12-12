using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introController : MonoBehaviour
{
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        delay = Time.time + 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && Time.time > delay)
        {
            PlayerPrefs.SetString("LastScene", "Menu");
            SceneManager.LoadScene("Menu");
        }
    }
}

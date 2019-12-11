using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperScript : MonoBehaviour
{
    gameController sceneManager;
    Button thisButton;

    void Start()
    {
        sceneManager = GameObject.Find("GameController").GetComponent<gameController>();
    }

    public void LoadScene(string name)
    {
        sceneManager.LoadScene(name);
    }

    public void LoadLastScene()
    {
        sceneManager.LoadLastScene();
    }
    
    public void incHead(int inc)
    {
        sceneManager.IncBodyPart(0, inc);
    }

    public void incBody(int inc)
    {
        sceneManager.IncBodyPart(1, inc);
    }

    public void incLegs(int inc)
    {
        sceneManager.IncBodyPart(2, inc);
    }
}

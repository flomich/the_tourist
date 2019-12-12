using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelperScript : MonoBehaviour
{
   // gameController sceneManager;
    Button thisButton;

    private int num_outfits = 2;

    void Start()
    {
        //sceneManager = GameObject.Find("GameController").GetComponent<gameController>();
    }

    public void LoadScene(string name)
    {
        PlayerPrefs.SetString("LastScene", name);
        SceneManager.LoadScene(name);
    }

    public void LoadLastScene()
    {
        string last_scene = PlayerPrefs.GetString("LastScene", "Menu");
        SceneManager.LoadScene(last_scene);
    }
    
    public void incHead(int inc)
    {
        int head_outfit = PlayerPrefs.GetInt("HeadOutfit", 0);
        PlayerPrefs.SetInt("HeadOutfit", (head_outfit + inc) % num_outfits);
        //sceneManager.IncBodyPart(0, inc);
    }

    public void incBody(int inc)
    {
        int body_outfit = PlayerPrefs.GetInt("BodyOutfit", 0);
        PlayerPrefs.SetInt("BodyOutfit", (body_outfit + inc) % num_outfits);
        //sceneManager.IncBodyPart(1, inc);
    }

    public void incLegs(int inc)
    {
        int legt_outfit = PlayerPrefs.GetInt("LegOutfit", 0);
        PlayerPrefs.SetInt("LegOutfit", (legt_outfit + inc) % num_outfits);
        //sceneManager.IncBodyPart(2, inc);
    }
}

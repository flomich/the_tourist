using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public int health;
    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        coins = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadCharSelect()
    {
        SceneManager.LoadScene("Char");
    }

    public void LoadLvlSelect()
    {
        health = 100;
        SceneManager.LoadScene("Map");
    }

}

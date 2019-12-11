using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public int health;
    public int coins;

    public int numOutfits;

    public int[] outfit;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        coins = 0;
        health = 100;
        outfit = new int[3];
        outfit[0] = 0;
        outfit[1] = 0;
        outfit[2] = 0;
        numOutfits = 2;
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

    public void LoadLvl(string name)
    {
        health = 100;
        SceneManager.LoadScene(name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void IncBodyPart(int body_part, int inc)
    {
        outfit[body_part] = (outfit[body_part] + inc) % numOutfits;
    }

}

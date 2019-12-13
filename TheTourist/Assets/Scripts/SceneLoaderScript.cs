using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public void loadScene(string name)
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(name);
    }

    public void loadLastScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene", "Menu"));
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}

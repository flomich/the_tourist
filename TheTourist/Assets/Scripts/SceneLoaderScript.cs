using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    private static float durationLastScene = 0;
    public void loadScene(string name)
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        durationLastScene = Time.timeSinceLevelLoad;
        SceneManager.LoadScene(name);
    }

    public void loadLastScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene", "Menu"));
    }

    public string getLastSceneName() {
        return PlayerPrefs.GetString("LastScene", "");
    }

    public void quitApplication()
    {
        Application.Quit();
    }

    public static float getDurationOfLastScene(){
        return durationLastScene;
    }
}

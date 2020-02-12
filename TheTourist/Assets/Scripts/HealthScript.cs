using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public float health = 100.0f;
    public float max_health = 100.0f;

    // Update is called once per frame
    void Update()
    {
        //check health and destroy game object (or other stuff)
        if(health <= 0 && gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void addHealth(float health_increment)
    {
        health = Mathf.Min(health + health_increment, max_health);
    }

    public void takeHealth(float health_decrement)
    {
        health = Mathf.Max(health - health_decrement, 0.0f);
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return max_health;
    }

    public bool hasFullHealth()
    {
        return max_health - health < 0.01f;
    }
}

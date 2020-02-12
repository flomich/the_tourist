using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public float health = 100.0f;
    public float max_health = 100.0f;

    private float game_over_timer = 2.0f;

    public void Start()
    {
        game_over_timer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //check health and destroy game object (or other stuff)
        if(health <= 0 && gameObject.tag == "Player")
        {
            // Player died, fall down and disable animations
            game_over_timer -= Time.deltaTime;

            // fall down
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
                Vector3 force = new Vector3(1.0f, 0.0f, 0.0f);
                rigidbody.AddForce(force);
                rigidbody.freezeRotation = false;
            }

            // disable movement and animations
            movePlayer move_script = gameObject.GetComponent<movePlayer>();
            if (move_script != null)
            {
                move_script.animator.SetInteger("WalkState", 0);
                move_script.enabled = false;
            }

            // display game over screen
            if (game_over_timer <= 0.0f)
            {
                SceneManager.LoadScene("GameOver");
            }
            
        }
    }

    public void addHealth(float health_increment)
    {
        health = Mathf.Min(health + health_increment, max_health);
    }

    public void takeHealth(float health_decrement)
    {
        if(gameObject.tag == "Player")
        {
            SoundEffectScript.Instance.playDamageSound(gameObject.transform.position);
        }
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

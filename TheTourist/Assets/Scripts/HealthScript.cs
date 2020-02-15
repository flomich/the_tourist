using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public float health = 100.0f;
    public float max_health = 100.0f;


    private float game_over_timer = 2.0f;

    private float collision_damage = 0.0f;

    public GameObject damage_icon;

    public void Start()
    {
        game_over_timer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(collision_damage > 0.0f)
        {
            takeHealth(collision_damage);
            collision_damage = 0.0f;
        }

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
                PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("GameOver");
            }
            
        }
    }

    public void addHealth(float health_increment)
    {
        health = Mathf.Min(health + health_increment, max_health);
    }

    private void playDamageEffects()
    {

        // Instantiate damage icon
        Quaternion rotation = Quaternion.identity;
        rotation.z = Random.Range(0.0f, 0.7f);
        Vector3 position = gameObject.transform.position + new Vector3(0.0f, 2.0f, 0.0f);

        CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();

        if (collider != null)
        {
            position.y += 1.0f;
        }

        // spwan damage icon
        GameObject icon = Instantiate(damage_icon, position, rotation);
        icon.transform.SetParent(gameObject.transform);

        if (gameObject.tag == "Player")
        {
            // spawn damage paticle effect
            ParticleEffectsScript.Instance.playerDamageEffect(position, icon);

            // spawn sound effect
            SoundEffectScript.Instance.playDamageSound(gameObject.transform.position);
        }
        else
        {
            // spawn enemy damage paticle effect
            ParticleEffectsScript.Instance.enemyDamageEffect(position, icon);
        }

        

        
    }

    public void takeHealth(float health_decrement)
    {
        playDamageEffects();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //only enable jumping if contact with ground
        for (int i = 0; i < collision.contactCount; i++)
        {
            //get contact point normal
            Vector2 normal = collision.GetContact(i).normal;

            float force = collision.GetContact(i).normalImpulse;

            if (force > 15.0f) collision_damage += 1.0f;
        }
    }
}

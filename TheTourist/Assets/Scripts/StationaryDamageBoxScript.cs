using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StationaryDamageBoxScript : MonoBehaviour
{
    public ParticleSystem particle_system;
    public AudioSource audio_source;
    public float damage = 0.0f;
    public float rest_interval = 1.0f;
    public float damage_time = 20.0f;

    private float timer = 0.0f;


    public void Update()
    {
        timer -= Time.deltaTime;

        // did the timer run out?
        if (timer <= 0.0f)
        {
            Collider2D collider = gameObject.GetComponent<Collider2D>();

            // is the collider currently enabled?
            if (collider.enabled)
            {
                // disable collider for rest interval seconds
                collider.enabled = false;
                timer = rest_interval;
            }
            else
            {
                // enable collider for damage time seconds
                collider.enabled = true;
                timer = damage_time;

                // spawn particle effect for damage time seconds
                ParticleEffectsScript.Instance.createParticleSystem(particle_system, transform.position, damage_time);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player_game_object = collision.gameObject;
        // is the other collider the player?
        if (player_game_object.tag.Contains("Player"))
        {
            // play Damage Sound
            SoundEffectScript.Instance.playAudioSource(audio_source, gameObject.transform.position);

            //get health script and apply damage to player
            HealthScript health_script = player_game_object.GetComponent<HealthScript>();
            if (health_script != null)
            {
                health_script.takeHealth(damage);
            }
        }
    }
}

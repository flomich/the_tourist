using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns effects and deals damage on collision 
public class StaticDamageBoxScript : MonoBehaviour
{
    public ParticleSystem particle_system;
    public AudioSource audio_source;
    public float damage = 10.0f;
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject player_game_object = collider.gameObject;
        // is player the other collider?
        if (player_game_object.tag.Contains("Player"))
        {
            // play damage sound
            SoundEffectScript.Instance.playAudioSource(audio_source, gameObject.transform.position);
            // spawn particle effect
            Collider2D my_collider = gameObject.GetComponent<Collider2D>();
            ParticleEffectsScript.Instance.createParticleSystem(particle_system, transform.position);

            // apply damage
            HealthScript health_script = player_game_object.GetComponent<HealthScript>();
            if (health_script != null)
            {
                health_script.takeHealth(damage);
            }
        }
    }
}

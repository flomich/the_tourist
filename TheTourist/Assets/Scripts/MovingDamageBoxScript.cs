using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// give damage or destroy self on collision
public class MovingDamageBoxScript : MonoBehaviour
{
    public ParticleSystem particle_system;
    public AudioSource audio_source;
    public float damage = 10.0f;
    public float max_impact = 2.0f;
    void OnCollisionEnter2D(Collision2D collison)
    {
        // is player the other collider?
        if (collison.gameObject.tag.Contains("Player"))
        {
            // apply damage
            HealthScript health_script = collison.gameObject.GetComponent<HealthScript>();
            if (health_script != null)
            {
                health_script.takeHealth(damage);
            }
            spawnEffects();
            // destroy self
            Destroy(gameObject);
        }
        else
        {
            if (collison.relativeVelocity.magnitude > max_impact)
            {
                spawnEffects();
                Destroy(gameObject);
            }
        }

    }

    private void spawnEffects()
    {
        // play damage sound
        SoundEffectScript.Instance.playAudioSource(audio_source, gameObject.transform.position);
        // spawn particle effect
        Collider2D my_collider = gameObject.GetComponent<Collider2D>();
        ParticleEffectsScript.Instance.createParticleSystem(particle_system, transform.position);
    }
}

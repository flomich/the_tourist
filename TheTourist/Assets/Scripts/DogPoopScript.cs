using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPoopScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other_collider)
    {
        GameObject player_game_object = other_collider.gameObject;
        if (player_game_object.tag.Contains("Player"))
        {
            SoundEffectScript.Instance.playStepInPoop(gameObject.transform.position);
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            Vector3 offset = new Vector3(0.0f, collider.bounds.max.y, 0.0f);
            ParticleEffectsScript.Instance.stinkClouds(transform.position + offset );

            HealthScript health_script = player_game_object.GetComponent<HealthScript>();
            if(health_script != null)
            {
                health_script.takeHealth(10);
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsScript : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static ParticleEffectsScript Instance;

    public ParticleSystem beer_explosion;
    public ParticleSystem beer_explosion_1;
    public ParticleSystem doener_explosion;
    public ParticleSystem doener_explosion_1;
    public ParticleSystem frankfurter_explosion;
    public ParticleSystem frankfurter_explosion_1;
    public ParticleSystem poop_effect;
    public ParticleSystem player_damage_effect;
    public ParticleSystem enemy_damage_effect;
    public ParticleSystem speed_boost_effect;
    public ParticleSystem damage_boost_effect;
    public ParticleSystem health_boost_effect;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of ParticleEffectsScript!");
        }

        Instance = this;
    }


    public void beerExplosion(Vector3 position)
    {
        instantiate(beer_explosion, position);
        instantiate(beer_explosion_1, position);
    }

    public void stinkClouds(Vector3 position)
    {
        instantiate(poop_effect, position);
    }

    public void doenerExplosion(Vector3 position)
    {
        instantiate(doener_explosion, position);
        instantiate(doener_explosion_1, position);
    }
    public void frankfurterExplosion(Vector3 position)
    {
        instantiate(frankfurter_explosion, position);
        instantiate(frankfurter_explosion_1, position);
    }

    public void playerDamageEffect(Vector3 position)
    {
        instantiate(player_damage_effect, position);
    }

    public void enemyDamageEffect(Vector3 position)
    {
        instantiate(enemy_damage_effect, position);
    }

    public void healthBoostEffect(Vector3 position, GameObject attached=null, float lifetime= 0.0f)
    {
        ParticleSystem system = instantiate(health_boost_effect, position, lifetime);
        
        if (attached != null)
        {
            system.transform.SetParent(attached.transform);
        }
    }

    public void damageBoostEffect(Vector3 position, GameObject attached = null, float lifetime=0.0f)
    {
        ParticleSystem system = instantiate(damage_boost_effect, position, lifetime);

        if (attached != null)
        {
            system.transform.SetParent(attached.transform);
        }
    }

    public void speedBoostEffect(Vector3 position, GameObject attached = null, float lifetime=0.0f)
    {
        ParticleSystem system = instantiate(speed_boost_effect, position, lifetime);

        if (attached != null)
        {
            system.transform.SetParent(attached.transform);
        }
    }

    public void createParticleSystem(ParticleSystem particle_system, Vector3 position, float lifetime)
    {
        if(particle_system != null)
        {
            instantiate(particle_system, position, lifetime);
        }
        
    }

    public void createParticleSystem(ParticleSystem particle_system, Vector3 position)
    {
        if (particle_system != null)
        {
            instantiate(particle_system, position);
        }

    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;


        // Destroy Particle System when lifetime is over
        Destroy(newParticleSystem.gameObject,
          newParticleSystem.main.duration) ;

        return newParticleSystem;
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position, float lifetime)
    {
        if(lifetime > 0.0f)
        {
            var main = prefab.main;
            main.duration = lifetime;
        }

        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;


        // Destroy Particle System when lifetime is over
        Destroy(newParticleSystem.gameObject,
          newParticleSystem.main.duration + 0.2f);

        return newParticleSystem;
    }
}

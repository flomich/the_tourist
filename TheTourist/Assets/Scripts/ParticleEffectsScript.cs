using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsScript : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static ParticleEffectsScript Instance;

    public ParticleSystem beerExplosion;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of ParticleEffectsScript!");
        }

        Instance = this;
    }


    public void BeerExplosion(Vector3 position)
    {
        instantiate(beerExplosion, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        newParticleSystem.playbackSpeed = 3.0f;

        // Destroy Particle System when lifetime is over
        Destroy(newParticleSystem.gameObject,
          newParticleSystem.main.duration) ;

        return newParticleSystem;
    }
}

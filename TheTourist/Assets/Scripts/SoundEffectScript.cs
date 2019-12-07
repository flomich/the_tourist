using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectScript Instance;

    public AudioSource breaking_glass_audio_source;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectScript!");
        }

        Instance = this;
    }


    public void playBreakingGlass(Vector3 position)
    {
        instantiate(breaking_glass_audio_source, position);
    }

    private AudioSource instantiate(AudioSource prefab, Vector3 position)
    {
        AudioSource new_audio_source = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as AudioSource;

        new_audio_source.Play();

        // Destroy Particle System when lifetime is over
        Destroy(new_audio_source.gameObject, 0.5f);

        return new_audio_source;
    }
}

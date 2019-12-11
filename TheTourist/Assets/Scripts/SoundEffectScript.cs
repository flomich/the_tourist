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
    public AudioSource breaking_doener_audio_source;
    public AudioSource breaking_frankfurter_audio_source;
    public AudioSource consume_puntigamer_audio_source;
    public AudioSource consume_doener_audio_source;
    public AudioSource consume_frankfurter_audio_source;
    public AudioSource upgrade_audio_source;
    public AudioSource ambient_audio_source;

    private GameObject player;

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

    public void playBreakingDoener(Vector3 position)
    {
        instantiate(breaking_doener_audio_source, position);
    }

    public void playBreakingFrankfurter(Vector3 position)
    {
        instantiate(breaking_frankfurter_audio_source, position);
    }

    public void playConsumePuntigamer(Vector3 position)
    {
        instantiate(consume_puntigamer_audio_source, position);
    }

    public void playConsumeDoener(Vector3 position)
    {
        instantiate(consume_doener_audio_source, position);
    }

    public void playConsumeFrankfurter(Vector3 position)
    {
        instantiate(consume_frankfurter_audio_source, position);
    }

    public void playUpgradeSound(Vector3 position)
    {
        instantiate(upgrade_audio_source, position);
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
        Destroy(new_audio_source.gameObject, 1.0f);

        return new_audio_source;
    }
}

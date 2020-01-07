using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutParticleEmission : MonoBehaviour
{
    private float timer;
    private int max_particles;
    void Start()
    {
        timer = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        ParticleSystem particle_system = GetComponent<ParticleSystem>();
        if (particle_system != null)
        {
            var main = particle_system.main;
            float f = 1.0f - (timer / main.duration);
            float scale = Mathf.SmoothStep(0.0f, 1.0f, f);
            var emission_module = particle_system.emission;
            emission_module.rateOverTime = main.maxParticles * f;
        }
    }
}

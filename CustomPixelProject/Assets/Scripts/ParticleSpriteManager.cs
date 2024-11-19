using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpriteManager : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public List<Sprite> particleSprites;
    public Transform StartPoint; 
    public Transform EndPoint;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();

        var textureSheetAnimation = ParticleSystem.textureSheetAnimation;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;
        textureSheetAnimation.RemoveSprite(0);

        //Add sprites from list to system
        foreach(var sprite in particleSprites)
        {
            textureSheetAnimation.AddSprite(sprite);
        }

        textureSheetAnimation.frameOverTime = new ParticleSystem.MinMaxCurve(1.0f, AnimationCurve.Linear(0, 0, 1, 1));

    }

    void Update()
    {
        //Move particles toward endpoint
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ParticleSystem.main.maxParticles];
        int numParticlesAlive = ParticleSystem.GetParticles(particles);

        for(int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 direction = (EndPoint.position - particles[i].position).normalized;
            float speed = 5f;

            Debug.DrawLine(particles[i].position, EndPoint.position, Color.red, 0.1f);
            particles[i].velocity = direction * speed;
        }

        ParticleSystem.SetParticles(particles, numParticlesAlive);

    }
}

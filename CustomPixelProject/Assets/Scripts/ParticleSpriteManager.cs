using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpriteSystem : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public List<Sprite> particleSprites;
    public Transform StartPoint; 
    public Transform EndPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Check if system is assigned
        if(ParticleSystem == null)
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            transform.position = StartPoint.position;
        }


        ParticleSystem.transform.position = StartPoint.position;
        var textureSheetAnimation = ParticleSystem.textureSheetAnimation;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;
        textureSheetAnimation.RemoveSprite(0);

        //Add sprites from list to system
        foreach(var sprite in particleSprites)
        {
            textureSheetAnimation.AddSprite(sprite);
        }

        

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
            particles[i].velocity = direction * speed;
        }

        ParticleSystem.SetParticles(particles, numParticlesAlive);

    }
}

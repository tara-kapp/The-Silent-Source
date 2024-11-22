using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpriteManager : MonoBehaviour
{
    public PigManager pigManager;
    public List<Sprite> manMadeItems;

    public ParticleSystem ParticleSystem;
    public List<Sprite> particleSprites;

    
    public Transform StartPoint; 
    public Transform EndPoint;

    // Start is called before the first frame update
    void Start()
    {
        manMadeItems = pigManager.getManMadeItems();
        for (int i = 0; i < manMadeItems.Count; i++)
        {
            Debug.Log("Ported List" + manMadeItems[i]);

        }

        ParticleSystem = GetComponent<ParticleSystem>();

        var textureSheetAnimation = ParticleSystem.textureSheetAnimation;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;
        //textureSheetAnimation.RemoveSprite(0);

        //SetParticleSprite();
        //Add sprites from list to system
        //foreach(var sprite in particleSprites)
        //{
        //    textureSheetAnimation.AddSprite(sprite);
        //}

        textureSheetAnimation.frameOverTime = new ParticleSystem.MinMaxCurve(1.0f, AnimationCurve.Linear(0, 0, 1, 1));

    }

    public void SetParticleSprite(Sprite sprite)
    {
        var texturesheet = ParticleSystem.textureSheetAnimation;
        texturesheet.RemoveSprite(0);
        texturesheet.AddSprite(sprite);
        Debug.Log("here");
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

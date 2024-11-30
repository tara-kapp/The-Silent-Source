using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleSpriteManager : MonoBehaviour
{
    public PigManager pigManager;
    public List<Sprite> manMadeItems;

    public ParticleSystem ParticleSystem;
      
    public Transform StartPoint; 
    public Transform EndPoint;

    public Sprite assignedItem;
    public GameObject ItemSpawn;
    public SpriteRenderer itemSprite;

    public spriteSwitch spriteSwitch;


    void Start()
    {
        
    }

    public void ShowItemSprite(Sprite sprite)
    {
        
        if(ItemSpawn != null)
        {
            SpriteRenderer itemSprite = ItemSpawn.AddComponent<SpriteRenderer>();
            Debug.Log(sprite.name);
            itemSprite.sprite = sprite;
            Debug.Log(sprite.name);
            spriteSwitch.CheckSprite(sprite);
        }
        


    }

    public void RemoveItemSprite(){
        Destroy(ItemSpawn);
        ItemSpawn = new GameObject("ItemSpawn");
        ItemSpawn.transform.Translate(0, 8, 0);
    }


    // Changes particle sprites based on pig item
    public void UpdateParticleSprite(Sprite sprite)
    {
        if (ParticleSystem != null && sprite != null )
        {
            var texturesheet = ParticleSystem.textureSheetAnimation;
             texturesheet.SetSprite(0, sprite);    
        }
        RemoveItemSprite();
        ShowItemSprite(sprite);
        
                
    }
    
    void Update()
    {              
        //Move particles toward endpoint
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ParticleSystem.main.maxParticles];
        int numParticlesAlive = ParticleSystem.GetParticles(particles);

        for(int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 direction = (EndPoint.position - particles[i].position).normalized;

            //Particle speed
            float speed = 6f;

            Debug.DrawLine(particles[i].position, EndPoint.position, Color.red, 0.1f);


            particles[i].velocity = direction * speed;
        }

        ParticleSystem.SetParticles(particles, numParticlesAlive);

    }
}

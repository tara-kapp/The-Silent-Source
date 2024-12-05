using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PigBehavior : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public ParticleSystem heartParticles;
    private SpriteRenderer pigSpriteRenderer;      
    public ParticleSpriteManager particleSpriteManager;    
    public Sprite assignedItem;
    public GameObject happyOverlay;


    void Start()
    {
        happyOverlay.SetActive(false);
        pigSpriteRenderer = GetComponent<SpriteRenderer>(); 
       
    }
    // Function occurs after pig reaches max damage
    public void TriggerExplosion()
    {
        
        if(particleSpriteManager != null)
        {
            // Assigns particles to cooresponding pig item
            particleSpriteManager.UpdateParticleSprite(assignedItem);            
        }

        // Instantiate particle explosion
        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();
        
        

        // Check to see if particles have ended
        if (explosionEffect.isStopped)
        {
            // Delay Coroutine
            StartCoroutine(DelayedFunction(5f));

            // Sets timer to delay pig movement
            IEnumerator DelayedFunction(float delay)
            {
                yield return new WaitForSeconds(delay);

                // Removes pig off screen
                FindObjectOfType<PigManager>().RemoveCurrentPig();
            }
        }        
    }

    public void TriggerHearts()
    {
        
        if (heartParticles != null)
        {
            happyOverlay.SetActive(true);
            heartParticles.Play();            
        }
    }
}

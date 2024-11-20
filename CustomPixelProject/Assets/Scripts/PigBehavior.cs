using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PigBehavior : MonoBehaviour
{

    public ParticleSystem explosionEffect;
    private SpriteRenderer pigSpriteRenderer;
    private Sprite assignedItem;
    public Sprite itemSpawn;
    public ParticleSpriteManager particleSpriteManager;


    void Start()
    {
        pigSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AssignItem(Sprite item)
    {
        assignedItem = item;        
    }
    

    public void TriggerExplosion()
    {        
        if(particleSpriteManager != null)
        {
            particleSpriteManager.SetParticleSprite(assignedItem);
        }


        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();
        
        //Debug.Log($"Reveleaing item: {assignedItem.name}");

        if (explosionEffect.isStopped)
        {
            StartCoroutine(DelayedFunction(10f));

            //Sets timer to delay pig movement
            IEnumerator DelayedFunction(float delay)
            {
                yield return new WaitForSeconds(delay);
                FindObjectOfType<PigManager>().RemoveCurrentPig();
            }

            

        }        
    }
}

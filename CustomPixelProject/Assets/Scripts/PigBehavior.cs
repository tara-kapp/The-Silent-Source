using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehavior : MonoBehaviour
{

    public ParticleSystem explosionEffect;
    private SpriteRenderer pigSpriteRenderer;
    private Sprite assignedItem;
    private int damageCount = 0;
    private int maxDamage = 5;
    private bool isExploded = false;

    
    void Start()
    {
        pigSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AssignItem(Sprite item)
    {
        assignedItem = item;
    }

    public void DamagePig()
    {
        if (isExploded) return;

        damageCount++;
        if(damageCount >= maxDamage )
        {
            TriggerExplosion();
        }
    }

    private void TriggerExplosion()
    {
        isExploded = true;
        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();

        Debug.Log($"Reveleaing item: {assignedItem.name}");

        FindObjectOfType<PigManager>().RemoveCurrentPig();
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}

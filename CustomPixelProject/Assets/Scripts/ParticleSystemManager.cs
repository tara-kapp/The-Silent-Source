using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public List<Sprite> particleSprites; 

    // Start is called before the first frame update
    void Start()
    {

        //Check if system is assigned
        if(ParticleSystem == null)
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        var textureSheetAnimation = ParticleSystem.textureSheetAnimation;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;
        textureSheetAnimation.RemoveSprite(0);

        //Add sprites from list to system
        foreach(var sprite in particleSprites)
        {
            textureSheetAnimation.AddSprite(sprite);
        }

        //Set particles to randomly choose
        textureSheetAnimation.frameOverTime = new ParticleSystem.MinMaxCurve(1.0f, AnimationCurve.Linear(0, 0, 1, 1));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

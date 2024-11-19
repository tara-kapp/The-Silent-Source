using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticles : MonoBehaviour
{

    public ParticleSystem ParticleSystem;
    public Texture2D spriteTexture;
    public float pixelSize = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateParticles();
        
    }


    void GenerateParticles()
    {
        int width = spriteTexture.width;
        int height = spriteTexture.height;
        int totalPixels = width * height;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[totalPixels];
        int index = 0;

        for(int y=0; y < height; y++)
        {
            for (int x=0; x < width; x++)
            {
                Color pixelColor = spriteTexture.GetPixel(x, y);

                //skip transparent pixels
                if(pixelColor.a > 0.1f)
                {
                    Vector3 position = new Vector3(x * pixelSize, y * pixelSize, 0);
                    particles[index].position = position;
                    particles[index].startColor = pixelColor;
                    particles[index].startSize = pixelSize;
                    index++;
                    Debug.Log("working");
                }
            }
        }

        GetComponent<ParticleSystem>().SetParticles(particles, index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01_Script : MonoBehaviour
{

    public Texture2D pigOutline;
    public List<Sprite> smallImageSprites;
    public float targetDenisty = 0.4f;
    public int cellSize = 20; 
    public float smallImageScale = 0.5f;

    private List<Vector2> placementCoords = new List<Vector2>();

    private void Start()
    {        
        FitCameraToTextureAndCenter(pigOutline);
        PopulatePigOutline();
    }

    void PopulatePigOutline()
    {
        //Loop through pig outline image
        int width = pigOutline.width;
        int height = pigOutline.height;
        int pixelsFilled = 0;
        int pixelsInOutline = 0;
        
        for(int y = 0; y < height; y += cellSize)
        { 
            for(int x = 0; x < width; x += cellSize)
            {
                // Check if this cell is within the outline
                if(IsInsidePigOutline(x,y))
                {
                    pixelsInOutline++;

                    //Random generator to place image or not
                    if(Random.value < 0.5f)
                    {
                        Vector2 position = new Vector2(x, y);
                        placementCoords.Add(position);
                        pixelsFilled++;
                    }
                }
            }
        }

        //Instantiate small images
        foreach(Vector2 coord in placementCoords)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(coord.x, coord.y, 10));
            GameObject smallImageInstance = new GameObject("SmallImage");

            var spriteRenderer = smallImageInstance.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = smallImageSprites[Random.Range(0, smallImageSprites.Count)];

            smallImageInstance.transform.position = worldPosition;
            smallImageInstance.transform.parent = this.transform;
            smallImageInstance.transform.localScale = Vector3.one * smallImageScale;
        }

    }

    void FitCameraToTextureAndCenter(Texture2D texture)
    {
        if (texture == null)
        {
            Debug.LogError("Texture is null. Assign a valid Texture2D.");
            return;
        }

        float pixelsPerUnit = 100f; // Match this to your project setup.
        float worldWidth = texture.width / pixelsPerUnit;
        float worldHeight = texture.height / pixelsPerUnit;

        float aspectRatio = (float)Screen.width / Screen.height;

        if (worldWidth / aspectRatio > worldHeight)
        {
            Camera.main.orthographicSize = (worldWidth / 2) / aspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = worldHeight / 2;
        }

        // Center the camera to align with the texture.
        Camera.main.transform.position = new Vector3(worldWidth / 2, worldHeight / 2, Camera.main.transform.position.z);
    }


    void CenterCameraOnTexture(Texture2D texture)
    {
        float worldWidth = texture.width / 100f; //Pixels per unit of pig
        float worldHeight = texture.height / 100f;

        // Center the camera based on the size of the texture.
        Camera.main.transform.position = new Vector3(worldWidth / 2, worldHeight / 2, Camera.main.transform.position.z);
        
    }

    bool IsInsidePigOutline(int x, int y)
    {
        //Check pixel color
        Color pixelColor = pigOutline.GetPixel(x, y);
        return pixelColor.r > 0.9f && pixelColor.g > 0.9f && pixelColor.b > 0.9f;
    }

}

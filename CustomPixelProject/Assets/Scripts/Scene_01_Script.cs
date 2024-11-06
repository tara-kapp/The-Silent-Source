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

    bool IsInsidePigOutline(int x, int y)
    {
        //Check pixel color
        Color pixelColor = pigOutline.GetPixel(x, y);
        return pixelColor.r > 0.9f && pixelColor.g > 0.9f && pixelColor.b > 0.9f;
    }

}

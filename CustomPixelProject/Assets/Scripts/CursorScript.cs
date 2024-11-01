using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Assign your sprite renderer with the sprite in the inspector
    public SpriteRenderer cursorSprite; 
    public Sprite knifeSprite;
    public Sprite magnifierSprite;

    private bool isKnifeMode = true;


    

    void Start()
    {
        // hide default cursor
        Cursor.visible = false;
        cursorSprite.sprite = knifeSprite;
    }

    void Update()
    {
        // Get the mouse position and convert it to world position
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Update the position of the cursor sprite
        cursorSprite.transform.position = cursorPos;

        if (Input.GetMouseButtonDown(0))
        {
            spriteSwap();
        }
    }

    void spriteSwap()
    {
        if (isKnifeMode)
        {
            cursorSprite.sprite = magnifierSprite; 
        }
        else
        {
            cursorSprite.sprite = knifeSprite;
        }


        isKnifeMode = !isKnifeMode;
    }
}

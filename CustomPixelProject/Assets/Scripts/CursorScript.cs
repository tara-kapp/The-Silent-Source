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
    public AudioSource sliceSound;

    public float rotationAngle = 25f;  // Amount of rotation when pressed
    public float rotationSpeed = 5f;  // Speed of rotation

    private Quaternion originalRotation;  // Store the original rotation
    private Quaternion targetRotation;    // Target rotation when mouse is pressed

    void Start()
    {

        
        cursorSprite.sprite = knifeSprite;
        Cursor.visible = false;
        
        // Set initial rotations
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, rotationAngle);  // Set the target rotation angle
    }

    void Update()
    {
        // Get the mouse position and convert it to world position
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Update the position of the cursor sprite
        cursorSprite.transform.position = cursorPos;

        if (Input.GetMouseButton(0)) // While the mouse is held down
        {
            // Start rotating
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            if (!sliceSound.isPlaying) // Play sound only if it's not already playing
            {
                sliceSound.Play();
            }
        }
        else
        {
            // Stop rotating and return to original rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
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

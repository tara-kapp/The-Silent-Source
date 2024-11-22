using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{



    public GameObject Cursor1; // Assign via Inspector
    public Button modeSwitchButton;   // Reference to the UI Button

    private SpriteRenderer spriteRenderer;
    public Sprite knifeSprite; // SpriteRenderer for the knife
    public Sprite handSprite;  // SpriteRenderer for the hand
    public Sprite handPettingSprite;  // SpriteRenderer for the hand
    public TrailRenderer trailRenderer;       // TrailRenderer for the trail
    public Gradient normalColor;             // Gradient for normal trail color
    public Gradient targetSpriteColor;       // Gradient for trail color when over target sprite

    public Transform knifeTransform;         // Transform for the knife
    public float normalSpeed = 10f;          // Normal movement speed
    public float swipeSpeed = 25f;           // Movement speed when mouse is pressed
    public float rotationAngle = 25f;        // Amount of rotation on mouse press
    public float rotationSpeed = 5f;         // Speed of rotation

    private Quaternion originalRotation;     // Original rotation of the knife
    private Quaternion targetRotation;       // Target rotation on mouse press
    public AudioSource sliceSound;

    public bool petMode;
    public bool isPetting = false;

    void Start()
    {

        petMode = false;

        modeSwitchButton.onClick.AddListener(handModeSwitch);
        // Initialize rotations
        originalRotation = knifeTransform.rotation;
        targetRotation = Quaternion.Euler(0, 0, rotationAngle);

        // Disable trail initially and set trail color
        trailRenderer.emitting = false;
        trailRenderer.colorGradient = normalColor; 


        // Initialize sprite render and configure sprites
        spriteRenderer = Cursor1.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = knifeSprite;
        
    }

    void Update()
    {
        // Get the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Determine movement speed based on mouse button state
        float currentSpeed = Input.GetMouseButton(0) ? swipeSpeed : normalSpeed;

        // Move the knife smoothly toward the cursor
        knifeTransform.position = Vector2.Lerp(knifeTransform.position, mousePosition, Time.deltaTime * currentSpeed);

        if ((Input.GetMouseButton(0)) && (!petMode)) 
        {
            sliceSound.Play();
            // Rotate the knife toward the target rotation
            knifeTransform.rotation = Quaternion.Lerp(knifeTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Enable the trail
            trailRenderer.emitting = true;

            // Perform a Raycast to detect target sprite
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("TargetSprite"))
            {
                // Change trail color when over the target sprite
                trailRenderer.colorGradient = targetSpriteColor;
            }
            else
            {
                // Revert to normal trail color
                trailRenderer.colorGradient = normalColor;
            }
        }
        else if ((Input.GetMouseButton(0)) && (petMode))
        {

            spriteRenderer.sprite = handPettingSprite;
        }
        else if ((!Input.GetMouseButton(0)) && (petMode))
        {

            spriteRenderer.sprite = handSprite;
            trailRenderer.emitting = false;
        }
        else if ((!Input.GetMouseButton(0)) && (!petMode))
        {

            // Reset the knife to its original rotation
            knifeTransform.rotation = Quaternion.Lerp(knifeTransform.rotation, originalRotation, Time.deltaTime * rotationSpeed);

            // Disable the trail
            trailRenderer.emitting = false;
        }
    }


    public void handModeSwitch()
    {
         // Toggle the state of petMode
        petMode = !petMode;

        // Change the cursor sprite based on the state
        if (petMode)
        {
            spriteRenderer.sprite = handSprite;
            Debug.Log("Pet mode activated");
        }
        else
        {
            spriteRenderer.sprite = knifeSprite;
            Debug.Log("Pet mode deactivated");
        }
    }


    public bool getMode()
    {
        return petMode;
    }

}


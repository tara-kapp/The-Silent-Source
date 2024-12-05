using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    public GameObject Cursor1; // Assign via Inspector
    public Button petModeSwitchButton;   // Reference to the UI Button
    public Button knifeModeSwitchButton;   // Reference to the UI Button

    private SpriteRenderer spriteRenderer;

    public PigManager pigMan;
    public cursorUI cursorButton;

    public Sprite knifeSprite; // SpriteRenderer for the knife
    public Sprite handSprite;  // SpriteRenderer for the hand
    public Sprite handPettingSprite;  // SpriteRenderer for the hand
    public Sprite magnifierSprite;  // SpriteRenderer for the hand


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

    public string defaultMode = "DEFAULT";
    public string petMode = "PETMODE";
    public string knifeMode = "KNIFEMODE";
    public string magnifierMode = "MAGNIFYMODE";

    public float petTime = 5f;

    public string modeState = "";

    public bool isPetting = false;
    public float pettingSpeed = 0.1f;
    public float pettingDist = 0.5f;

    public PigBehavior pigBehavior;

    void Start()
    {

        // Listening for buttons
        petModeSwitchButton.onClick.AddListener(petModeToggle);
        knifeModeSwitchButton.onClick.AddListener(knifeModeToggle);
        // Initialize rotations
        originalRotation = knifeTransform.rotation;
        targetRotation = Quaternion.Euler(0, 0, rotationAngle);
        // Disable trail initially and set trail color
        trailRenderer.emitting = false;
        trailRenderer.colorGradient = normalColor; 
        // Initialize sprite render and configure sprites
        spriteRenderer = Cursor1.GetComponent<SpriteRenderer>();
        activateDefaultMode();
    }

    void Update()
    {
        // Get the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Perform a Raycast to detect target sprites
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (!pigMan.getIfPigInPos() || !cursorButton.isDecisionMade())
        {
            activateDefaultMode();
        }

        if (hit.collider != null && hit.collider.CompareTag("ObjectOfInterest"))
        {
            activateMagnifyMode();
            Debug.Log("Object Identified!.klj");
        }
        if(modeState == knifeMode)
        {
            // Determine movement speed based on mouse button state
            float currentSpeed = Input.GetMouseButton(0) ? swipeSpeed : normalSpeed;
            // Move the knife smoothly toward the cursor
            knifeTransform.position = Vector2.Lerp(knifeTransform.position, mousePosition, Time.deltaTime * currentSpeed);
        }

        else
        {
            Cursor1.transform.position = mousePosition;
        }
        if ((Input.GetMouseButton(0)) && (modeState == knifeMode)) 
        {
            sliceSound.Play();
            // Rotate the knife toward the target rotation
            knifeTransform.rotation = Quaternion.Lerp(knifeTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Enable the trail
            trailRenderer.emitting = true;

            // Perform a Raycast to detect target sprite
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
        else if ((Input.GetMouseButton(0)) && (modeState == petMode) && !isPetting)
        {

            spriteRenderer.sprite = handPettingSprite;
            StartCoroutine(PettingMotion());
        }
        else if ((!Input.GetMouseButton(0)) && (modeState == petMode))
        {

            spriteRenderer.sprite = handSprite;
            trailRenderer.emitting = false;
        }
        else if ((!Input.GetMouseButton(0)) && (modeState != petMode))
        {
            // Reset the knife to its original rotation
            knifeTransform.rotation = Quaternion.Lerp(knifeTransform.rotation, originalRotation, Time.deltaTime * rotationSpeed);
            // Disable the trail
            trailRenderer.emitting = false;
        }
    }

// Petting Animation while hand hovers over 
public IEnumerator PettingMotion()
{
    isPetting = true;
    pigBehavior.TriggerHearts();    

        // Offset for downward motion
        Vector3 offset = new Vector3(0, -pettingDist, 0);

    while (Input.GetMouseButton(0) && modeState == petMode)
    {
        // Continuously track the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Keep it on the 2D plane

        // Calculate the downward and upward positions relative to the current mouse position
        Vector3 downPosition = mousePosition + offset;

        // Move the hand down
        float elapsedTime = 0f;
        while (elapsedTime < pettingSpeed && Input.GetMouseButton(0))
        {
            Cursor1.transform.position = Vector3.Lerp(mousePosition, downPosition, elapsedTime / pettingSpeed);
            elapsedTime += Time.deltaTime;

            // Update mouse position dynamically while animating
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            downPosition = mousePosition + offset;

            yield return null;
        }

        // Move the hand back up
        elapsedTime = 0f;
        while (elapsedTime < pettingSpeed && Input.GetMouseButton(0))
        {
            Cursor1.transform.position = Vector3.Lerp(downPosition, mousePosition, elapsedTime / pettingSpeed);
            elapsedTime += Time.deltaTime;

            // Update mouse position dynamically while animating
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            downPosition = mousePosition + offset;

            yield return null;
        }
    }

    isPetting = false;
}


// UI Toggle Methods with cursorUI integration
public void petModeToggle()
{
    if (modeState != petMode)
    {
            activatePetMode();
    }
    else
    {
            activateKnifeMode();
        }

    //Debug.Log("STATE: " + getMode());

}

public void knifeModeToggle()
{
    if (modeState != knifeMode)
    {
            activateKnifeMode();
    }
    else
    {
            activatePetMode();
    }
    //Debug.Log("STATE: " + getMode());
}

public string getMode()
{
    return modeState;
}
// Mode Activation Methods
public void activateKnifeMode()
    {
        Cursor.visible = false;
        modeState = knifeMode;
        spriteRenderer.sprite = knifeSprite;
    }

public void activatePetMode()
    {
        Cursor.visible = false;
        modeState = petMode;
        spriteRenderer.sprite = handSprite;
        Invoke("activateKnifeMode", petTime);
        
    }

public void activateDefaultMode()
    {
        Cursor.visible = true;
        modeState = defaultMode;
        spriteRenderer.sprite = null;
    }
public void activateMagnifyMode()
    {
        Cursor.visible = false;
        modeState = magnifierMode;
        spriteRenderer.sprite = magnifierSprite;
    }
}



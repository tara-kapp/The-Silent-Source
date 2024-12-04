using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // For managing scenes

public class ConveyerRise : MonoBehaviour
{
    public GameObject spriteObject;  // The sprite you want to move
    public float moveSpeed = 2f;  // Speed of movement
    public Vector3 targetPosition;  // Final position of the sprite
    private Vector3 startPosition;  // Initial position of the sprite

    void Start()
    {
        // Check if the current scene is "Cut_Scene2.0"
        if (SceneManager.GetActiveScene().name == "Cut_Scene2.0")
        {
            Debug.Log("Cut_Scene2.0 detected. Starting conveyor belt.");
            
            // Ensure the spriteObject is set before proceeding
            if (spriteObject != null)
            {
                startPosition = spriteObject.transform.position;  // Get initial position of the sprite
                StartCoroutine(MoveSprite());  // Start the conveyor movement
            }
            else
            {
                Debug.LogError("SpriteObject is not assigned!");
            }
        }
    }

    private IEnumerator MoveSprite()
    {
        // Calculate the journey length (distance between start and target positions)
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        // Move the sprite from start position to target position
        while (Time.time < startTime + journeyLength / moveSpeed)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            spriteObject.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        // Ensure the sprite reaches the target position
        spriteObject.transform.position = targetPosition;
    }
}
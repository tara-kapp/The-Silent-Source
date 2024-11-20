using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class ConveyerRise : MonoBehaviour
{
    private VideoPlayer videoPlayer;  // Reference to the VideoPlayer
    public GameObject spriteObject;  // The sprite you want to move
    public float moveSpeed = 2f;  // Speed of movement
    public Vector3 targetPosition;  // Final position of the sprite
    private Vector3 startPosition;  // Initial position of the sprite

    void Start()
    {
        // Find the "Cutscene01" GameObject and get its VideoPlayer component
        videoPlayer = GameObject.Find("Cutscene01").GetComponent<VideoPlayer>();
        
        // Ensure the spriteObject is set before proceeding
        if (spriteObject != null)
        {
            startPosition = spriteObject.transform.position;  // Get initial position of the sprite
        }
        else
        {
            Debug.LogError("SpriteObject is not assigned!");
            return;
        }

        // Trigger the move when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Cutscene has finished, starting sprite move.");
        // Start the sprite move
        StartCoroutine(MoveSprite());
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
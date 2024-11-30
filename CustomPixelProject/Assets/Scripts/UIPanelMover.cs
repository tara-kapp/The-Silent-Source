using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMover : MonoBehaviour
{
    public RectTransform panel;        // Reference to the UI panel
    public Vector2 targetPosition;     // Reference to the target UI element (where the panel moves to)
    public Vector2 originalPosition;   // Stores the panel's original position
    public PigManager pigMan;
    public cursorUI cursorButton;
    public float moveSpeed = 5f;       // Speed of movement

    private bool isInPos  = false;
    private bool isMoving = false;

    private void Start()
    {
        originalPosition = panel.anchoredPosition;
    }

    public void Update()
    {

        if (pigMan.getIfPigInPos() && !isInPos && !cursorButton.isDecisionMade())
        {
            if (!isMoving)
            {
                MovePanelToTarget();
            }
        }
        else if ((!pigMan.getIfPigInPos() || cursorButton.isDecisionMade()) && isInPos)
        {
            if (!isMoving)
            {
                MovePanelToOriginal();
            }
        }
    }

    // Call this to start the movement sequence
    public void MovePanelToTarget()
    {
        StartCoroutine(MoveToPosition(panel, targetPosition, () =>
        {
            isInPos = true;
        }));
    }

    // Start moving the panel back to the original position
    public void MovePanelToOriginal()
    {
        StartCoroutine(MoveToPosition(panel, originalPosition, () =>
        {
            isInPos = false;
        }));
    }

    // Coroutine to move the panel
    private IEnumerator MoveToPosition(RectTransform panel, Vector2 targetPos, System.Action onComplete = null)
    {

        isMoving = true;
        while (Vector2.Distance(panel.anchoredPosition, targetPos) > 0.1f)
        {
            // Smoothly move the panel towards the target position
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Snap the position exactly to the target
        panel.anchoredPosition = targetPos;

        if (panel.anchoredPosition == targetPos)
        {
            isInPos = true; 
        }

        // Call the onComplete action if specified
        onComplete?.Invoke();

        isMoving = false;
    }
}

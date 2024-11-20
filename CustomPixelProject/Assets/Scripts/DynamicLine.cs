using UnityEngine;

public class DynamicLine : MonoBehaviour
{
    public LineRenderer lineRendererPrefab; // Prefab for the LineRenderer
    public SpriteRenderer spriteRenderer;  // Target SpriteRenderer
    private LineRenderer currentLineRenderer;
    private Vector3 previousMousePosition;
    private bool isDrawing = false;

    void Update()
    {
        // Check if the mouse is within the sprite bounds
        if (IsMouseInSprite())
        {
            if (Input.GetMouseButtonDown(0)) // Start of a slash
            {
                StartNewSlash();
            }
            else if (Input.GetMouseButton(0) && isDrawing) // Continue drawing
            {
                AddPointToSlash(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else if (Input.GetMouseButtonUp(0)) // End of the slash
            {
                EndSlash();
            }
        }
        else if (isDrawing) // Stop drawing if mouse exits the sprite
        {
            EndSlash();
        }
    }

    bool IsMouseInSprite()
    {
        // Get mouse position in world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Ensure z is 0 for 2D calculations

        // Get the sprite's bounds
        Bounds bounds = spriteRenderer.bounds;

        // Check if the mouse position is within the sprite's bounds
        return bounds.Contains(mouseWorldPos);
    }

    void StartNewSlash()
    {
        // Instantiate a new LineRenderer for the slash
        currentLineRenderer = Instantiate(lineRendererPrefab, transform);
        currentLineRenderer.positionCount = 0; // Initialize with no points
        isDrawing = true;

        // Record the initial mouse position
        previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        previousMousePosition.z = 0; // Ensure z is 0
        AddPointToSlash(previousMousePosition);
    }

    void AddPointToSlash(Vector3 point)
    {
        // Ensure the z-coordinate is 0 for 2D drawing
        point.z = 0;

        // Add the point to the current LineRenderer
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, point);

        // Update the previous mouse position
        previousMousePosition = point;
    }

    void EndSlash()
    {
        // Stop drawing
        isDrawing = false;
    }
}

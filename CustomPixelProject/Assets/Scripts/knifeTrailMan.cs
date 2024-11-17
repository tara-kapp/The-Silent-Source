using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeTrailMan : MonoBehaviour
{
    public LineRenderer lineRenderer; // Assign via Inspector or GetComponent
    public float maxLineLength = 10f; // Maximum length of the line
    public float lineLifetime = 2f;   // How long the line stays visible

    private float currentLineLength = 0f; // Tracks the current length of the line
    private float lineExpireTime = 0f;    // When the line should expire
    private Vector3 lastPosition;         // Tracks the last position

   void Awake()
{
   if (lineRenderer == null)
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }
}

    void Start()
    {
        lineRenderer.positionCount = 0; // Start with an empty line
    }

    void Update()
    {
        // Check if the line's lifetime has expired
        if (Time.time > lineExpireTime && lineRenderer.positionCount > 0)
        {
            ClearLine(); // Clear the line when expired
        }
    }

    public void StartLine(Vector3 startPosition)
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPosition);
        lastPosition = startPosition;
        currentLineLength = 0f;
        lineExpireTime = Time.time + lineLifetime; // Reset expiry
    }

    public void UpdateLine(Vector3 newPosition)
    {
        float segmentLength = Vector3.Distance(newPosition, lastPosition);

        // Only add a new point if within the max length
        if (currentLineLength + segmentLength <= maxLineLength)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            currentLineLength += segmentLength;
            lastPosition = newPosition;
            lineExpireTime = Time.time + lineLifetime; // Extend expiry
        }
    }

    public void ClearLine()
    {
        lineRenderer.positionCount = 0; // Clear all points
        currentLineLength = 0f;
    }
}

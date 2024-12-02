using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTimer : MonoBehaviour
{

    private float elapsedTime = 15f;
    // Update is called once per frame
    void Update()
    {
        // Increment elapsed time
        elapsedTime += Time.deltaTime;
        Debug.Log("Elapsed Time: " + elapsedTime.ToString("F2") + " seconds");

    }
}

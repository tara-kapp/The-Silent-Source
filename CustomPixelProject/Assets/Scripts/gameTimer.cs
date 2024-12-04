using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameTimer : MonoBehaviour
{

    private static float elapsedTime = 0f;
    private static int pigsKilledPerSec = 46;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        Scene currentScene = SceneManager.GetActiveScene();
        
        // Display the scene's name in the console
        if (currentScene.name == "End_Cutscene")
        {
            Debug.Log("TimePassed " + elapsedTime.ToString());
        }

    }

    public float getTimeElapsed()
    {
        return elapsedTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pigsKilledUpdater : MonoBehaviour
{

    public TextMeshProUGUI tmpText;
    public gameTimer gameTimer;

    private static int pigsKilledPerSec = 46;

    public GameObject endButton;

    // Initialize the default text properties
    public void Start()
    {

         tmpText.text = "Pigs Killed...";

    }
    public void Update()
    {
        float elapsedTime = gameTimer.getTimeElapsed();
        float pigsKilledCalc = elapsedTime * pigsKilledPerSec;
        int pigSlain = (int)pigsKilledCalc;
        tmpText.text = "Pigs Killed..." + pigSlain.ToString();
        if( pigSlain >= 2750 ){
            endButton.SetActive(true);
        }
    }
}

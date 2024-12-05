using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TypingFXProFREE;

public class endSceneTextHandler : MonoBehaviour
{

    public TextMeshProUGUI pigsKilledCounter;
    public TextMeshProUGUI pigsKilledTimerMins;
    public TextMeshProUGUI pigsKilledTimerSecs;
    public TextMeshProUGUI pigsKilledContext;


    public gameTimer gameTimer;

    private static int pigsKilledPerSec = 46;

    public GameObject endButton;

    public TextDisplay textDisplay;

 
    // Initialize the default text properties
    public void Start()
    {

        pigsKilledCounter.text = "";
        pigsKilledTimerMins.text = "";
        pigsKilledTimerSecs.text = "";
        pigsKilledContext.text = "";


        textDisplay.TriggerSelectedEffect();


    }
    public void Update()
    {
        float elapsedTime = gameTimer.getTimeElapsed();
        float pigsKilledCalc = elapsedTime * pigsKilledPerSec;
        int pigSlain = (int)pigsKilledCalc;

        int timeMins = (int)(elapsedTime / 60f);
        int timeSecs = (int)(elapsedTime % 60f);

        
        pigsKilledCounter.text = pigSlain.ToString();
        pigsKilledTimerMins.text =  timeMins.ToString("00") + " MINS";
        pigsKilledTimerSecs.text =  timeSecs.ToString("00") + " SECS";



        //pigsKilledContext.text = "PIGS HAVE BEEN <color=#FF1100>SLAUGHTERED</color> FOR...";
        if( pigSlain >=5 ){
            endButton.SetActive(true);
        }

        
    }


} 
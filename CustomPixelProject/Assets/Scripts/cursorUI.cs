using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorUI : MonoBehaviour
{

    public CursorScript cursorScript; // Reference to the CursorScript

    public Button petModeButton;  // Reference to the UI Button
    public Button knifeModeButton;  // Reference to the UI Button

    public bool decisionMade = false;

    void Start()
    {

        petModeButton.onClick.AddListener(petSwitchMode);
        knifeModeButton.onClick.AddListener(knifeSwitchMode);

        decisionMade = false;


    }

    // Update is called once per frame
    public void petSwitchMode()
    {
        cursorScript.petModeToggle();
        decisionMade = true;

    }

    public void knifeSwitchMode()
    {

        cursorScript.knifeModeToggle();
        decisionMade = true;

    }

    public bool isDecisionMade()
    {
        return decisionMade;
    }

    public void resetDecisionMade()
    {
        decisionMade = false;
    }


}

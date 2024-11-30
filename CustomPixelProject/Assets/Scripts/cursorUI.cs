using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorUI : MonoBehaviour
{

    public CursorScript cursorScript; // Reference to the CursorScript

    public Button petModeButton;  // Reference to the UI Button
    public Button knifeModeButton;  // Reference to the UI Button

    void Start()
    {

        petModeButton.onClick.AddListener(petSwitchMode);
        knifeModeButton.onClick.AddListener(knifeSwitchMode);
        
        
    }

    // Update is called once per frame
    public void petSwitchMode()
    {
        cursorScript.petModeToggle();
 
    }

    public void knifeSwitchMode()
    {

        cursorScript.knifeModeToggle();
 
    }


}

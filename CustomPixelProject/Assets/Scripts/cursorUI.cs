using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorUI : MonoBehaviour
{

    public CursorScript cursorScript; // Reference to the CursorScript

    public Button modeSwitchButton;  // Reference to the UI Button

    void Start()
    {

        modeSwitchButton.onClick.AddListener(SwitchMode);
        
    }

    // Update is called once per frame
    public void SwitchMode()
    {
        cursorScript.handModeSwitch();
    }
}

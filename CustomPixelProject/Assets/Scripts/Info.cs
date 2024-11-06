using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    public string message;
    private void OnMouseEnter()
    {
        HoverPopUp._instance.SetandShowToolTip(message);
        Debug.Log("Cursor Detected");
        Debug.Log(message);

    }

    private void OnMouseExit()
    {
       HoverPopUp._instance.HideToolTip();
    }

 
}

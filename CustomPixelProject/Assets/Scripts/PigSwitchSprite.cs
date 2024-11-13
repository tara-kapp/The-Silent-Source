using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSwitchSprite : MonoBehaviour
{
    public GameObject painOverlay;   // Overlay that displays pain effect
    private bool knifeInRange = false;
    public AudioSource pigPain;

    void Start()
    {
        // Ensure the overlay starts off (inactive)
        if (painOverlay != null)
        {
            painOverlay.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        knifeInRange = true;
    }

    private void OnMouseExit()
    {
        knifeInRange = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && knifeInRange)
        {
            Debug.Log("PIG CUT!");

            if (painOverlay != null)
            {
                // Toggle the overlay on each click
                painOverlay.SetActive(!painOverlay.activeSelf);
                pigPain.Play();
            }
        }
    }
}

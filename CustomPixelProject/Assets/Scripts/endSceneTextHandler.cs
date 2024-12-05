using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endSceneTextHandler : MonoBehaviour
{
    public TextMeshProUGUI pigsKilledCounter;
    public TextMeshProUGUI pigsKilledTimerMins;
    public TextMeshProUGUI pigsKilledTimerSecs;
    public TextMeshProUGUI pigsKilledContext;
    public TextMeshProUGUI manMadeItem;

    public gameTimer gameTimer;

    private static int pigsKilledPerSec = 46;

    public GameObject endButton;


    private List<string> items = new List<string>
    {
        "Bullets", "Shampoo", "Upholstery", "Chalk", "Crayons", "Pet Food",
        "Fabrics", "Fertilizers", "Football", "Candles", "Glue", "Heart Valves",
        "Insulin", "Jello", "Leather", "Matches", "Medicine", "Paintbrushes",
        "Cork", "Luggage", "Bone China"
    };

    private int currentIndex = 0;
    private float itemDisplayInterval = 2.0f; // Time in seconds between item updates
    private float itemTimer = 0f;

    // Initialize the default text properties
    public void Start()
    {
        pigsKilledCounter.text = "";
        pigsKilledTimerMins.text = "";
        pigsKilledTimerSecs.text = "";
        pigsKilledContext.text = "";
        manMadeItem.text = "";

        ShuffleList(items);
    }

    public void Update()
    {
        // Update elapsed time and pigs killed calculations
        float elapsedTime = gameTimer.getTimeElapsed();
        float pigsKilledCalc = elapsedTime * pigsKilledPerSec;
        int pigSlain = (int)pigsKilledCalc;

        int timeMins = (int)(elapsedTime / 60f);
        int timeSecs = (int)(elapsedTime % 60f);

        pigsKilledCounter.text = pigSlain.ToString();
        pigsKilledTimerMins.text = timeMins.ToString("00") + " MINS";
        pigsKilledTimerSecs.text = timeSecs.ToString("00") + " SECS";

        if (pigSlain >= 5)
        {
            endButton.SetActive(true);
        }

        // Update the displayed man-made item
        itemTimer += Time.deltaTime;
        if (itemTimer >= itemDisplayInterval)
        {
            itemTimer = 0f;
            pickItem();
        }
    }

    // Display the current item and move to the next one
    public void pickItem()
    {
        if (items.Count == 0) return;

        manMadeItem.text = items[currentIndex];
        currentIndex = (currentIndex + 1) % items.Count; // Cycle back to the start
    }

    // Shuffle the list of items
    private void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTypes : MonoBehaviour
{

    public TextMeshProUGUI manMadeItem;


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

    public void Start()
    {

        // Shuffle items for randomness
        ShuffleList(items);
    }

    public void Update()
    {
        // Update elapsed time and pig-related text


        // Update the displayed man-made item at intervals
        UpdateManMadeItem();
    }



    private void UpdateManMadeItem()
    {
        itemTimer += Time.deltaTime;
        if (itemTimer >= itemDisplayInterval)
        {
            itemTimer = 0f; // Reset timer

            // Update man-made item text and cycle through items
            manMadeItem.text = items[currentIndex];
            currentIndex = (currentIndex + 1) % items.Count; // Cycle back to the start if at the end
        }
    }

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


  

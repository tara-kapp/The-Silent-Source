using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spriteSwitch : MonoBehaviour
{
    public Texture[] spriteArray;
    public int spriteNum;
    public RawImage sprite;
    public TMP_Text messageText;
    private string[] descriptions = {"Bullets", "Buttons", "Upholstery", "Chalk", "Crayons", "Pet Food", 
    "Fabrics", "Fertilizers", "Football", "Gloves", "Glue", "Heart Valves", "Insulin", "Jello", "Leather",
    "Matches", "Medicine", "Paintbrushes", "Purses", "Luggage", "Bone China"};

    public GameObject gameobject;

    // Update is called once per frame

    void Start(){
        //gameobject.SetActive(false);
    }

    void Update(){
        
    }

    public void OnClick(){
        UpdateSprite();
        gameobject.SetActive(true);
        
    }

    public void OnExit(){
        gameobject.SetActive(false);
    }
    void UpdateSprite() {
        sprite.texture = spriteArray[spriteNum];
        messageText.SetText(descriptions[spriteNum]);
    }
}

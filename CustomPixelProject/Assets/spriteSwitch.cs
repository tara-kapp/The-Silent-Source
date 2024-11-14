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
    public string[] stringArray;

    public GameObject gameobject;
    public GameObject[] products;

    // Update is called once per frame

    void Start(){
        gameobject.SetActive(false);
        spriteNum = 0;
    }

    void Update(){
        
    }

    public void OnClick(){
        products[spriteNum].SetActive(false);
        UpdateSprite();
        gameobject.SetActive(true);
        
    }

    public void OnExit(){
        gameobject.SetActive(false);
        spriteNum++;
        products[spriteNum].SetActive(true);

    }
    void UpdateSprite() {
        sprite.texture = spriteArray[spriteNum];
        messageText.SetText(stringArray[spriteNum]);
    }
}

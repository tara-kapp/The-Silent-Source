using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spriteSwitch : MonoBehaviour
{
    public Texture[] spriteArray;
    public int spriteNum = 0;
    public RawImage sprite;
    public TMP_Text messageText;
    public string[] stringArray;

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }

    void UpdateSprite() {
        sprite.texture = spriteArray[spriteNum];
        messageText.SetText(stringArray[spriteNum]);
    }
}

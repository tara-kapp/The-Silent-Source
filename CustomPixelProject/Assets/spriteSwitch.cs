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

    public TMP_Text headingText;
    public TMP_Text messageText;

    public int[] fontsize;

    private string[] headings = {
        "Bullets",
        "Shampoo",
        "Upholstery",
        "Chalk",
        "Crayons",
        "Pet Food",
        "Fabrics",
        "Fertilizers",
        "Football",
        "Candles",
        "Glue",
        "Heart Valves",
        "Insulin",
        "Jello",
        "Leather",
        "Matches",
        "Medicine",
        "Paintbrushes",
        "Cork",
        "Luggage",
        "Bone China"
    };
    private string[] descriptions = {
        "Bone gelatine used to help transport the gunpowder or cordite into the casing", 
        "Fatty acids from bone fat are used to give shampoo a pearl-like appearance.", 
        "Pig hair is commonly used in upholstery as a soft, work-friendly material for stuffing. A blend of 80% pig hair and 20% horse mane is often used as a second layer of stuffing.", 
        "Chalk can utilize pig products, specifically pig bones, which are processed into 'bone char' - a material used as a filler and sometimes as a whitening agent in the production.", 
        "Fatty acids are used as a hardening agent", 
        "liver, tripe, pig's trotters, udders and chicken feet are commonly used in pet food. These ingredients provide an excellent source of protein, amino acids and other nutrients.", 
        "Coating fabrics with processed pig fats, which are positively charged and cling to fabric surfaces, result in feeling soft and slippery.", 
        "composting their manure, which can be used to improve soil quality and promote healthy plant growth. Fertilizer can also be made from processed pig hair.", 
        "The first footballs were made from inflated pig bladders, which is where the term 'pigskin' comes from. Pig bladders were a good choice because they were cheap and durable.",  
        "Fatty acids from bone fat are used to stiffen the wax and raise the candle's melting point.",
        "Animal glue is a water-based adhesive that's non-toxic and biodegradable. It's used in traditional woodworking and painting techniques.", 
        "Under sterile conditions, the valves are removed from the pig's body. The excess tissue is removed, then the valves are 'resized' to be implanted into a human.", 
        "Taken from the pancreas, as closest to human in chemical structure.", 
        "Pigs are used to make gelatin, the main ingredient in Jell-O, by boiling their skin, bones, tendons, and ligaments to extract collagen", 
        "Pigs are used to make leather through a process that involves harvesting, processing, and tanning their skin",
        "Proteins found in pig bones are used to make and adhesive called 'Bone Glue'. When combined with flammable chemicals like phosphorus it can be used to make matches.", 
        "Pigs are a source of many medicines, including insulin, heparin, ACTH, and thyroid medications. Gelatine is used in the shell of medicine tablets to give it hardness.", 
        "Their coarse hairs, known as hog bristles, are strong, springy, and flexible, making them ideal for holding paint. The bristles also have natural split ends.", 
        "Bone gelatin is used as a binder in the process of making cork.", 
        "Pig leather has been used to make suitcases for a long time because of its durability, flexibility, and water resistance.", 
        "Dried and powdered bones from pigs are used to create 'Bone Glass', known as Porcelain or Ceramic pottery."
        };

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
        Debug.Log(spriteNum);
        
    }

    public void OnExit(){
        gameobject.SetActive(false);
    }
    void UpdateSprite() {
        sprite.texture = spriteArray[spriteNum];
        messageText.SetText(descriptions[spriteNum]);
        headingText.SetText(headings[spriteNum]);
        messageText.fontSize = fontsize[spriteNum];
    }
}

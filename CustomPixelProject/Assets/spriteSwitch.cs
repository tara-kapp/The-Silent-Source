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
    private string[] descriptions = {
        "Bullets<br>Bone gelatine used to help transport the gunpowder or cordite into the casing", 
        "Buttons<br>", 
        "Upholstery<br> Pig hair is commonly used in upholstery as a soft, work-friendly material for stuffing. A blend of 80% pig hair and 20% horse mane is often used as a second layer of stuffing.Upholstery fabric is typically heavyweight, thick, and stiff, with woven patterns rather than dyed-in patterns. It's used to cover furniture that has been stuffed with padding, springs, foam, or cushions.", 
        "Chalk<br>", 
        "Crayons<br>Fatty acids are used as a hardening agent", 
        "Pet Food<br>liver, tripe, pig's trotters, udders and chicken feet are commonly used in pet food. These ingredients provide an excellent source of protein, essential amino acids and other valuable nutrients.", 
        "Fabrics<br>Coating fabrics with processed pig fats, which are positively charged and cling to fabric surfaces, result in feeling soft and slippery.", 
        "Fertilizers<br>composting their manure, which can be used to improve soil quality and promote healthy plant growth. Fertilizer can also be made from processed pig hair.", 
        "Football<br>Early football. The first footballs were made from inflated pig bladders, which is where the term 'pigskin' comes from. Pig bladders were a good choice because they were cheap, easy to get, and durable.",  
        "Gloves",
        "Glue<br>Animal glue is a water-based adhesive that's non-toxic and biodegradable. It's used in traditional woodworking and painting techniques. In woodworking, it's reversible because it's water-soluble. In painting, it's used as a size for canvas and boards, and in tempera paints.", 
        "Heart Valves<br>The pigs that are used for medical purposes are grown for human consumption. Under sterile conditions, the valves are removed from the pig's body. The excess tissue and myocardium are then removed. The valves are then “sized” so they are appropriately fit when implanted into a human.", 
        "Insulin<br>Taken from the pancreas, as closest to human in chemical structure.", 
        "Jello<br>Pigs are used to make gelatin, the main ingredient in Jell-O, by boiling their skin, bones, tendons, and ligaments to extract collagen", 
        "Leather<br>Pigs are used to make leather through a process that involves harvesting, processing, and tanning their skin",
        "Matches<br>", 
        "Medicine<br>Pigs are a source of many medicines, including insulin, heparin, ACTH, and thyroid medications. Gelatine is used in the shell of medicine tablets to give it hardness.", 
        "Paintbrushes<br> their coarse hairs, also known as hog bristles, are strong, springy, and flexible, making them ideal for holding paint. The bristles also have natural split ends, which helps maintain the brush's precision.", 
        "Purses<br>", 
        "Luggage<br>Pig leather has been used to make suitcases for a long time because of its durability, flexibility, and water resistance. Pigskin suitcases were popular in the 19th century, and remained a symbol of reliability and traditional craftsmanship into the 20th century.", 
        "Bone China<br>Dried and powdered bones from pigs are used to create 'Bone Glass', more widely known as Porcelain or Ceramic pottery. Some of these pieces of 'bone glass' have a 30% concentration of the phosphates derived from the dried/powdered bones."
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
    }
}

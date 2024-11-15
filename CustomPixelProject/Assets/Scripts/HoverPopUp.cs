using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverPopUp : MonoBehaviour{


    public static HoverPopUp _instance; 
    public TextMeshProUGUI textComponent;
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    void Start()
    {
        // Hide the pop-up box at the start
        //Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        //transform.position = Input.mousePosition;
    }

    public void SetandShowToolTip(string message)
    {
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }

    
}

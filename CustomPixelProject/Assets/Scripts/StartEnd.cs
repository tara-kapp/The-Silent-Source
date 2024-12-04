using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEnd : MonoBehaviour
{
    public void LoadStartPage(){
        Debug.Log("click");
        SceneManager.LoadScene("Start");
    }

    public void LoadIntro(){
        Debug.Log("click");
        SceneManager.LoadScene("Intro_Cutscene(NEW)");
    }
}
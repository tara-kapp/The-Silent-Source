using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public float delay = 5f;
    public string nextSceneName = "Cut_Scene2.0";

    void Start()
    {
        Invoke("LoadNextScene", delay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

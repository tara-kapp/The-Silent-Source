using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneEndHandler : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public GameObject Cutscene01;  // Reference to the cutscene GameObject

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Cutscene01.SetActive(false);  // Disables the cutscene GameObject
    }
}
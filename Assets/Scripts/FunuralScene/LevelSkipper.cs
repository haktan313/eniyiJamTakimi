using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelSkipper : MonoBehaviour
{

    VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += SceneChange;
    }


    void SceneChange(VideoPlayer vp)
    {
      SceneManager.LoadScene(2);
    }
}
    


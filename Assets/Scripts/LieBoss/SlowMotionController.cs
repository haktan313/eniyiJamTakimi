using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionController : MonoBehaviour
{
    public float slowMotionScale = 0.5f; 
    public float slowMotionDuration = 2.0f; 

    private float originalTimeScale;
    private float slowMotionEndTime;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    public void StartSlowMotion()
    {
        Time.timeScale = slowMotionScale;
        slowMotionEndTime = Time.time + slowMotionDuration;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = originalTimeScale;
    }
}

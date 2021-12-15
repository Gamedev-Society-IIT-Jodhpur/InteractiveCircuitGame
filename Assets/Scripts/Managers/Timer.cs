using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float currentTime = 0;
    public static bool isTimerRunning = false;
    

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
        }
    }

    public static void StartTimer()
    {
        isTimerRunning = true;
    }

    public static void StopTimer()
    {
        isTimerRunning = false;
    }

    public static void SkipTime(float time)
    {
        currentTime += time;
    }

}

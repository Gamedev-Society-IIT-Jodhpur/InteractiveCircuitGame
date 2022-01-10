using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float currentTime = 0;
    public static bool isTimerRunning = false;
    public static int startMin;
    public static int startSec;
    public static int startHr;
    public static int startDay;
    public static int startMonth;
    int timeElapsed;


    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed = ((int.Parse(System.DateTime.Now.ToString().Substring(0, 2))-startDay)*24*60*60 +
                (int.Parse(System.DateTime.Now.ToString().Substring(11, 2)) - startHr) * 60 * 60) +
                ((int.Parse(System.DateTime.Now.ToString().Substring(14, 2)) - startMin) * 60) +
                (int.Parse(System.DateTime.Now.ToString().Substring(17, 2)) - startSec);
            if (timeElapsed >= 1)
            {
                currentTime += timeElapsed;
                startMin = int.Parse(System.DateTime.Now.ToString().Substring(14, 2));
                startSec = int.Parse(System.DateTime.Now.ToString().Substring(17, 2));
                startHr = int.Parse(System.DateTime.Now.ToString().Substring(11, 2));
            }
        }
    }


    public static void StartTimer()
    {
        isTimerRunning = true;
        startMin = int.Parse(System.DateTime.Now.ToString().Substring(14, 2));
        startSec = int.Parse(System.DateTime.Now.ToString().Substring(17, 2));
        startHr = int.Parse(System.DateTime.Now.ToString().Substring(11, 2));
        startDay = int.Parse(System.DateTime.Now.ToString().Substring(0, 2));
        //startMonth=System.DateTime.m

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

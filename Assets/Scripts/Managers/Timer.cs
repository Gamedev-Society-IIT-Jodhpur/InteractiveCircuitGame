using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime = 0;
    public bool isTimerRunning = false;
    public static Timer instance;
    float timeElapsed;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentTime = 0;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 1)
            {
                currentTime += timeElapsed;
                timeElapsed = 0;
            }
        }
    }


    public void StartTimer()
    {
        isTimerRunning = true;
        timeElapsed = 0;
        currentTime = 0;

        //startMonth=System.DateTime.m

    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void SkipTime(float time)
    {
        currentTime += time;
    }
}

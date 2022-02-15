using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    TMP_Text timeText;
    int min;
    int sec;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        min = (int)Timer.currentTime / 60;
        sec = (int)Timer.currentTime % 60;
        if (min < 10)
        {
            if (sec < 10)
            {
                timeText.text = "0" + min.ToString() + " : " + "0" + sec;
            }
            else
            {
                timeText.text = "0" + min.ToString() + " : " + sec;
            }
        }
        else
        {
            if (sec < 10)
            {
                timeText.text = min.ToString() + " : " + "0" + sec;
            }
            else
            {
                timeText.text = min.ToString() + " : " + sec;
            }
        }
        Timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        min = (int)Timer.currentTime / 60;
        sec = (int)Timer.currentTime % 60;
        if (min < 10)
        {
            if (sec < 10)
            {
                timeText.text = "0" + min.ToString() + " : " + "0" + sec;
            }
            else
            {
                timeText.text = "0" + min.ToString() + " : " + sec;
            }
        }
        else
        {
            if (sec < 10)
            {
                timeText.text = min.ToString() + " : " + "0" + sec;
            }
            else
            {
                timeText.text = min.ToString() + " : " + sec;
            }
        }


        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            //Time.timeScale *= 2;
            Timer.StartTimer();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Timer.StopTimer();
        }*/
    }
}

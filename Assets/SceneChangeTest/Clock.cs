using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = ((int)Timer.currentTime/60).ToString() +" : "+ (int)Timer.currentTime%60;

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Timer.StartTimer();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
           Timer.StopTimer();
        }
    }
}

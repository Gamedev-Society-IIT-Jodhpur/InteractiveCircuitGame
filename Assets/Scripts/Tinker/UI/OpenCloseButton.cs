using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenCloseButton : MonoBehaviour
{
    [SerializeField]RectTransform panel;
    bool isOpening = false;
    bool isClosing = false;
    float x;
    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            x = panel.anchoredPosition.x;
            x -= (2000) * Time.deltaTime;
            if (x <= -175)
            {
                x = -175;
                isOpening = false;
            }
            panel.anchoredPosition = new Vector2(x, panel.anchoredPosition.y);
        }
        if (isClosing)
        {
            x = panel.anchoredPosition.x;
            x += 2000 * Time.deltaTime;
            if (x >= 175)
            {
                x = 175;
                isClosing = false;
            }
            panel.anchoredPosition = new Vector2(x, panel.anchoredPosition.y);
        }
    }

    public void Toggle()
    {
        //panel.anchoredPosition=new Vector2(-panel.anchoredPosition.x, panel.anchoredPosition.y);
        if (panel.anchoredPosition.x > 0)
        {
            isOpening = true;
            GetComponentInChildren<TMP_Text>().text = ">";
        }
        else
        {
            isClosing = true;
            GetComponentInChildren<TMP_Text>().text = "<";

        }
    }
}

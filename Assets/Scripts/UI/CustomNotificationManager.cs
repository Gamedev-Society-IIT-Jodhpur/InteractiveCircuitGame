using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomNotificationManager : MonoBehaviour
{
    public static CustomNotificationManager Instance { get; private set; }

    public List<Sprite> icons;
    public GameObject notification;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNotification(int idx, string message)
    {
        GameObject tempNotification = Instantiate(notification, transform);

        Color iconColor = Color.white;

        if (idx == 0)
        {
            iconColor = Color.blue;
        }
        else if (idx == 1)
        {
            iconColor = Color.yellow;
        }
        else
        {
            iconColor = Color.red;
        }

        tempNotification.transform.GetChild(0).GetComponent<Image>().color = iconColor;
        tempNotification.transform.GetChild(0).GetComponent<Image>().sprite = icons[idx];
        tempNotification.transform.GetChild(1).GetComponent<TMP_Text>().text = message;
    }
}

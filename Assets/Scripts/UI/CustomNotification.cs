using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNotification : MonoBehaviour
{
    public float notificationTime = 3.0f;
    private void OnEnable()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        gameObject.GetComponent<CanvasGroup>().LeanAlpha(1, notificationTime).setOnComplete(removeNotification);
    }

    private void removeNotification()
    {
        gameObject.GetComponent<CanvasGroup>().LeanAlpha(0, 0.3f).setOnComplete(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);   
    }
}

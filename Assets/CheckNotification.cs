using System.Collections;
using UnityEngine;

public class CheckNotification : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(check());
    }

    IEnumerator check()
    {
        CustomNotificationManager.Instance.AddNotification(2, "This is an error message");
        yield return new WaitForSeconds(2.0f);

        CustomNotificationManager.Instance.AddNotification(0, "This is an normal message");
        yield return new WaitForSeconds(2.0f);

        CustomNotificationManager.Instance.AddNotification(1, "This is an warning message");
        yield return new WaitForSeconds(2.0f);

        CustomNotificationManager.Instance.AddNotification(2, "This is an error message");
        yield return new WaitForSeconds(2.0f);

        CustomNotificationManager.Instance.AddNotification(0, "This is an normal message");
        yield return new WaitForSeconds(3.0f);

        CustomNotificationManager.Instance.AddNotification(1, "This is an warning message");
        yield return new WaitForSeconds(4.0f);
    }
}

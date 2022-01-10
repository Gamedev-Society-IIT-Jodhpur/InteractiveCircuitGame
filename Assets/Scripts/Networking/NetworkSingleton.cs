using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSingleton : MonoBehaviour
{
    public static NetworkSingleton Instance { get; private set; }

    public int value;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CheckNetworkConnection()
    {
        //REACHABLE VIA LOCAL NETWORK OR CARRIER DATA
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            CustomNotificationManager.Instance.AddNotification(1, "No Internet Connection");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetXp()
    {
        string email = PlayerPrefs.GetString("player_email", "");
        StartCoroutine(SetXpOfUser(email));
    }

    IEnumerator SetXpOfUser(string email)
    {
        int xp = (int)MoneyAndXPData.xp;
        int totalXP = xp + PlayerPrefs.GetInt("player_xp");
        WWWForm postError = new WWWForm();

        postError.AddField("xp", totalXP);
        postError.AddField("email", email);

        UnityWebRequest uwr = UnityWebRequest.Post(AvailableRoutes.setXP, postError);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Can't submit XP");
        }
    }

}

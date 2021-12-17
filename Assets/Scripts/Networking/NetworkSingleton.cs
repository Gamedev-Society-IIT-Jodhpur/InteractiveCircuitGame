using UnityEngine;

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
            Debug.LogWarning("Connected to WiFi or Carrier network");
            CustomNotificationManager.Instance.AddNotification(2, "No Internet Connection");
            return true;
        }
        else
        {
            return false;
        }
    }
}

using UnityEngine;

public class TimeandMoneyManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

}

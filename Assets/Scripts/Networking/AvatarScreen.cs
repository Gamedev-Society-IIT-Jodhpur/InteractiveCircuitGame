using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarScreen : MonoBehaviour
{
    public GameObject[] avatars;
    int oldavatar = 0; 

    private void Awake()
    {
        oldavatar = PlayerPrefs.GetInt("player_avatar", 0);
    }

    private void Start()
    {
        avatars[oldavatar].GetComponent<RawImage>().color = new Color32(25, 156, 252, 255);
    }

    public void setAvatar(int index)
    {
        avatars[oldavatar].GetComponent<RawImage>().color = new Color32(37, 37, 92, 255);
        avatars[index].GetComponent<RawImage>().color = new Color32(25, 156, 252, 255);
        PlayerPrefs.SetInt("player_avatar", index);
        CustomNotificationManager.Instance.AddNotification(0, "New avatar set");
        oldavatar = index;
    }

    public void setNewAvatar()
    {
        StartCoroutine(onSetNewAvatar());
    }
     
    IEnumerator onSetNewAvatar()
    {

        Debug.Log(AvailableRoutes.updateUser + "?email=" + PlayerPrefs.GetString("player_email", "") + "&avatar=" + PlayerPrefs.GetInt("player_avatar"));
         

        UnityWebRequest www = UnityWebRequest.Post(AvailableRoutes.updateUser + "?email=" + PlayerPrefs.GetString("player_email", "") + "&avatar=" + PlayerPrefs.GetInt("player_avatar") , "");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("SetAvatar");
        }
    }
}

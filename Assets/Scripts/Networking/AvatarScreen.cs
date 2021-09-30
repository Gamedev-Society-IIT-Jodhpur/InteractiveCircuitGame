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
        oldavatar = PlayerPrefs.GetInt("avatar", 0);
    }

    private void Start()
    {
        avatars[oldavatar].GetComponent<RawImage>().color = new Color32(25, 156, 252, 255);
    }

    public void setAvatar(int index)
    {
        avatars[oldavatar].GetComponent<RawImage>().color = new Color32(37, 37, 92, 255);
        avatars[index].GetComponent<RawImage>().color = new Color32(25, 156, 252, 255);
        PlayerPrefs.SetInt("avatar", index);
        oldavatar = index;
    }

    public void setNewAvatar()
    {
        StartCoroutine(onSetNewAvatar());
    }
     
    IEnumerator onSetNewAvatar()
    {

        Debug.Log(AvailableRoutes.updateUser + "?email=" + PlayerPrefs.GetString("email", "") + "&avatar=" + PlayerPrefs.GetInt("avatar"));
         

        UnityWebRequest www = UnityWebRequest.Post(AvailableRoutes.updateUser + "?email=" + PlayerPrefs.GetString("email", "") + "&avatar=" + PlayerPrefs.GetInt("avatar") , "");

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

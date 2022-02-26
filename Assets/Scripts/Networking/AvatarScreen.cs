using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class AvatarScreen : MonoBehaviour
{
    public GameObject[] avatars;
    public TMP_InputField username;
    int oldavatar = 0;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
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
        oldavatar = index;
        //print(username.text);
        PlayerPrefs.SetInt("player_avatar", oldavatar);
    }

    public void setNewAvatar()
    {
        if (username.text == "") return;
        StartCoroutine(onSetNewAvatar());
    }

    IEnumerator onSetNewAvatar()
    {
        UnityWebRequest www = UnityWebRequest.Post(AvailableRoutes.updateUser + "?email=" + PlayerPrefs.GetString("player_email", "") + "&avatar=" + PlayerPrefs.GetInt("player_avatar"), "");

        
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Avatar not set");
            //Debug.Log(www.error);
        }
        else
        {
            PlayerPrefs.SetInt("player_avatar", oldavatar);
            PlayerPrefs.SetString("player_username", username.text);
            LoadingManager.instance.LoadGame(SceneIndexes.AvatarSelection, SceneIndexes.MainMenu);
        }
    }
}

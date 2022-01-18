using SimpleJSON;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleLogin : MonoBehaviour
{
    public GameObject emailInputField;

    private void Awake()
    {
        if (NetworkSingleton.Instance.CheckNetworkConnection())
        {
            StartCoroutine(GetXP());
        }
    }

    private void Start()
    {
        if (NetworkSingleton.Instance.CheckNetworkConnection())
        {
            if (PlayerPrefs.GetString("player_email", "") != "")
            {
                LoadingManager.instance.LoadGame(SceneIndexes.Login, SceneIndexes.MainMenu);
            }
        }
    }

    private IEnumerator GetXP()
    {
        UnityWebRequest xp = UnityWebRequest.Get(AvailableRoutes.getXP + PlayerPrefs.GetString("player_email", ""));
        yield return xp.SendWebRequest();

        if (xp.result != UnityWebRequest.Result.Success)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Can't get XP");
        }
        else
        {
            JSONNode data = JSON.Parse(xp.downloadHandler.text);
            PlayerPrefs.SetInt("player_xp", data["xp"]);
        }
    }

    public void setEmail()
    {
        string text = emailInputField.GetComponent<TMP_InputField>().text;
        PlayerPrefs.SetString("player_email", text);
    }

    public void onLogin()
    {
        StartCoroutine(onLoginToServer());
    }

    IEnumerator onLoginToServer()
    {
        UnityWebRequest www = UnityWebRequest.Get(AvailableRoutes.checkUser + emailInputField.GetComponent<TMP_InputField>().text);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Login Error");
        }
        else
        {
            JSONNode data = JSON.Parse(www.downloadHandler.text);
            PlayerPrefs.SetInt("player_avatar", data["avatar"]);
            PlayerPrefs.SetInt("player_xp", data["xp"]);
            Debug.Log("player_xp: " + data["xp"]);
            setEmail();

            LoadingManager.instance.LoadGame(SceneIndexes.Login, SceneIndexes.AvatarSelection);
        }
    }

    public void onGoogleSignup()
    {
        Application.OpenURL(AvailableRoutes.googleSignup);
    }
}

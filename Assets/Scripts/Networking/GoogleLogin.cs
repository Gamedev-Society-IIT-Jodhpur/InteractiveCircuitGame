using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class GoogleLogin : MonoBehaviour
{ 
    public GameObject emailInputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEmail(string email)
    {
        string text = emailInputField.GetComponent<TMP_InputField>().text; 
        PlayerPrefs.SetString("email", text);
    }

    public void onLogin()
    { 
        StartCoroutine(onLoginToServer());
    }


    IEnumerator onLoginToServer()
    { 
        Debug.Log(AvailableRoutes.checkUser + PlayerPrefs.GetString("email", ""));
        UnityWebRequest www = UnityWebRequest.Get(AvailableRoutes.checkUser + PlayerPrefs.GetString("email", ""));
        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {

            JSONNode data = JSON.Parse(www.downloadHandler.text);

            PlayerPrefs.SetInt("avatar", data["avatar"]);
        }
    }

    public void onGoogleSignup()
    {
        Application.OpenURL(AvailableRoutes.googleSignup);
    }
}

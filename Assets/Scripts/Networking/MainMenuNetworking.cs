using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using SimpleJSON;

public class MainMenuNetworking : MonoBehaviour
{
    public Text startButtonText;

    private bool isGameAvailable = false;

    private void Awake()
    {
        startButtonText.text = "Check Games";
    }

    public void CheckAvailableGames(string sceneToChange)
    {

        if (isGameAvailable)
        {
            SceneManager.LoadScene(sceneToChange);
        }
        else
        {
            StartCoroutine(CheckGames());
        }
    }

    IEnumerator CheckGames()
    {
        UnityWebRequest www = UnityWebRequest.Get(AvailableRoutes.availableGames);
        UnityWebRequestAsyncOperation asyncLoad = www.SendWebRequest();

        while (!asyncLoad.isDone)
        {
            startButtonText.text = "Checking...";
            yield return null;
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Network Error");
        }
        else
        {
            JSONNode gameData = JSON.Parse(www.downloadHandler.text);
            print(gameData.AsArray.Count);
            if(gameData.AsArray.Count > 0)
            {
                startButtonText.text = "Start";
                isGameAvailable = true;
            }
            else
            {
                CustomNotificationManager.Instance.AddNotification(0, "No game available");
                startButtonText.text = "Check Games";
            }
        }

        yield return new WaitForSeconds(1.0f);
    }

}
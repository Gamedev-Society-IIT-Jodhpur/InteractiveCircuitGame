using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenuNetworking : MonoBehaviour
{
    public Text startButtonText;

    private bool isGameAvailable = false;

    private void Awake()
    {
        startButtonText.text = "Check Games";
    }

    public void CheckAvailableGames(string toscene)
    {

        if (isGameAvailable)
        {
            //SceneManager.LoadScene(sceneToChange);
            LoadingManager.instance.LoadGame(SceneIndexes.MainMenu, toscene);
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
            //print(gameData.AsArray.Count);
            if (gameData.AsArray.Count > 0)
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

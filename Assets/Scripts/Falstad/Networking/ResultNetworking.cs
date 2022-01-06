using SimpleJSON;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResultNetworking : MonoBehaviour
{
    //string uri = "http://localhost:4040/api/result/userResults";
    //string uri = "http://localhost:4040/api/result/allResults";
    string addResult = AvailableRoutes.addResult;
    string allResults = AvailableRoutes.allResults;
    public GameObject loadingAnimation;

    [SerializeField]
    GameObject ContentView;

    [SerializeField]
    GameObject ListElement;

    void Start()
    {
        // TODO: To be removed------------------
        //PlayerPrefs.SetString("email", "sanodariya.1@iitj.ac.in");
        //AddResult();
        // -------------------------------------
        StartCoroutine(GetResult());
    }

    IEnumerator GetResult()
    {
        string requestUri = allResults + "?date=" + System.DateTime.Now.ToString("dd/MM/yyyy");
        UnityWebRequest www = UnityWebRequest.Get(requestUri);
        UnityWebRequestAsyncOperation asyncLoad = www.SendWebRequest();

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loadingAnimation.SetActive(false);

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            JSONNode data = JSON.Parse(www.downloadHandler.text);
            int i = 1;
            //Debug.Log(data);
            foreach (var item in data)
            {
                bool isUser = item.Value["email"] == PlayerPrefs.GetString("player_email");
                AddToList(i.ToString(), item.Value["name"] + " (" + item.Value["rollNo"] + ")", item.Value["score"], item.Value["xp"], isUser);
                i++;
            }
        }
    }

    void AddToList(string number, string name, string score, string xp, bool isUser)
    {
        GameObject ParentGameObject = Instantiate(ListElement, ContentView.transform);

        ParentGameObject.transform.SetParent(ContentView.transform);

        GameObject ChildGameObject1 = ParentGameObject.transform.GetChild(0).gameObject;
        GameObject ChildGameObject1_1 = ChildGameObject1.transform.GetChild(0).gameObject;
        ChildGameObject1_1.GetComponent<TextMeshProUGUI>().text = number;

        GameObject ChildGameObject2 = ParentGameObject.transform.GetChild(1).gameObject;
        GameObject ChildGameObject2_2 = ChildGameObject2.transform.GetChild(0).gameObject;
        ChildGameObject2_2.GetComponent<TextMeshProUGUI>().text = name;

        GameObject ChildGameObject3 = ParentGameObject.transform.GetChild(2).gameObject;
        GameObject ChildGameObject3_3 = ChildGameObject3.transform.GetChild(0).gameObject;
        ChildGameObject3_3.GetComponent<TextMeshProUGUI>().text = score;

        GameObject ChildGameObject4 = ParentGameObject.transform.GetChild(3).gameObject;
        GameObject ChildGameObject4_4 = ChildGameObject4.transform.GetChild(0).gameObject;
        ChildGameObject4_4.GetComponent<TextMeshProUGUI>().text = xp;

        if (isUser)
        {
            ChildGameObject1.GetComponent<RawImage>().color = new Color32(249, 127, 81, 255);
            ChildGameObject2.GetComponent<RawImage>().color = new Color32(249, 127, 81, 255);
            ChildGameObject3.GetComponent<RawImage>().color = new Color32(249, 127, 81, 255);
            ChildGameObject4.GetComponent<RawImage>().color = new Color32(249, 127, 81, 255);
        }

    }
}

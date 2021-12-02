using SimpleJSON;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ResultNetworking : MonoBehaviour
{
    //string uri = "http://localhost:4040/api/result/userResults";
    //string uri = "http://localhost:4040/api/result/allResults";
    string uri = AvailableRoutes.allResults;

    [SerializeField]
    GameObject ContentView;

    [SerializeField]
    GameObject ListElement;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        string requestUri = uri + "?date=" + System.DateTime.Now.ToString("dd/MM/yyyy");
        UnityWebRequest www = UnityWebRequest.Get(requestUri);

        //var asyncOperation = www.SendWebRequest();
        yield return www.SendWebRequest();

        //while (!asyncOperation.isDone)
        //{
        //    // wherever you want to show the progress: 
        //    //http://localhost:4040/api/result/allResults?date=26-09-2021
        //    //Debug.Log(asyncOperation.progress);

        //    //yield return null;
        //    // or if you want to stick doing it in certain intervals
        //    yield return new WaitForSeconds(0.05f);
        //}
        
        
        Debug.Log(www.downloadHandler.text);


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            JSONNode data = JSON.Parse(www.downloadHandler.text);
            int i = 1;
            Debug.Log(data);
            foreach (var item in data)
            {
                Debug.Log(item);
                AddToList(i.ToString(), item.Value["name"] + " (" + item.Value["rollNo"] + ")", item.Value["score"]);
                i++;
            }
        }
    }

    void AddToList(string number, string name, string score)
    {
        GameObject ParentGameObject = Instantiate(ListElement, ContentView.transform.position, ContentView.transform.rotation);
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
    }
}

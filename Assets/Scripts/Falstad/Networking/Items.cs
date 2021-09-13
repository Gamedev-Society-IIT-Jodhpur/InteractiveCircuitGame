using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class Items : MonoBehaviour
{

    //string uri = "http://localhost:4040/api/result/userResults";
    string uri = "http://localhost:4040/api/result/allResults";
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
        string uuId = "a1925e56-3401-44f4-aa1a-3424db830cab";
        string requestUri = uri /*+ "/" + uuId*/;
        UnityWebRequest www = UnityWebRequest.Get(requestUri);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        { 
            JSONNode data = JSON.Parse(www.downloadHandler.text);
            int i = 1;
            foreach (var item in data)
            { 
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Store : MonoBehaviour
{

    public static List<string> Items;

    public string url = "http://localhost:4040/api/item/availableItems";

    void Start()
    {
        Items = new List<string>();

        //StartCoroutine(getData());
        Debug.Log("yo");
    }

    IEnumerator getData()
    {
        UnityWebRequest itemsListRequest = UnityWebRequest.Get(url);

        yield return itemsListRequest.SendWebRequest();

        if (itemsListRequest.isNetworkError || itemsListRequest.isHttpError)
        {
            Debug.LogError(itemsListRequest.error);
            yield break;
        }

        JSONNode itemsListData = JSON.Parse(itemsListRequest.downloadHandler.text);
        Debug.Log(itemsListData["data"]);
    }

    void Update()
    {

    }
}

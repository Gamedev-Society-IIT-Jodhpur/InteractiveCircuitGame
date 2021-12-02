using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shelf : MonoBehaviour
{
    public GameObject ItemTemplate;
    public TextAsset csvFile;


    public string url = "http://localhost:4040/api/item/availableItems";

    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        Debug.Log("yo");
        UnityWebRequest itemsListRequest = UnityWebRequest.Get(url);

        yield return itemsListRequest.SendWebRequest();

        if (itemsListRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(itemsListRequest.error);
        }
        else
        {

            JSONNode itemsListData = JSON.Parse(itemsListRequest.downloadHandler.text);
            GameObject g;
            foreach (var item in itemsListData["data"])
            {
                g = Instantiate(ItemTemplate, transform);
                g.gameObject.SetActive(true);
                g.transform.GetChild(0).GetComponent<TMP_Text>().text = item.Value["name"];
                Texture itemTexture = StoreAssetmanager.Instance.getItemIcon(item.Value["name"]);
                g.transform.GetChild(1).GetComponent<RawImage>().texture = itemTexture;
                g.GetComponent<TooltipTrigger>().itemID = item.Value["id"].ToString();
            }

            StoreAssetmanager.Instance.itemsAvailable = itemsListData["data"];
        }

        yield return new WaitForSeconds(5);
    }
}

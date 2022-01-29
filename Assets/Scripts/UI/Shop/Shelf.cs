using SimpleJSON;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shelf : MonoBehaviour
{
    public GameObject ItemTemplate;

    private bool dataLoaded = false;

    [SerializeField]
    private GameObject loadingWheel;

    void Start()
    {
        StartCoroutine(getData());
    }

    private void Update()
    {
        if (dataLoaded)
        {
            loadingWheel.SetActive(false);
        }
    }

    IEnumerator getData()
    {
        UnityWebRequest itemsListRequest = UnityWebRequest.Get(AvailableRoutes.availableShopItems);
        //UnityWebRequest itemsListRequest = UnityWebRequest.Get(AvailableRoutes.availableItems);

        dataLoaded = false;

        UnityWebRequestAsyncOperation asyncLoad = itemsListRequest.SendWebRequest();

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        dataLoaded = true;

        if (itemsListRequest.result != UnityWebRequest.Result.Success)
        {
            //Debug.Log(itemsListRequest.error);
            CustomNotificationManager.Instance.AddNotification(2, "Can't get data");
        }
        else
        {
            JSONNode itemsListData = JSON.Parse(itemsListRequest.downloadHandler.text);
            GameObject g;
            foreach (var item in itemsListData["data"])
            {
                //print(item);
                g = Instantiate(ItemTemplate, transform);
                g.gameObject.SetActive(true);
                g.transform.GetChild(0).GetComponent<TMP_Text>().text = item.Value["type"];
                Sprite itemTexture = StoreAssetmanager.Instance.getItemIcon(item.Value["type"]);
                //Debug.Log(itemTexture);
                g.transform.GetChild(1).GetComponent<Image>().sprite = itemTexture;
                g.GetComponent<TooltipTrigger>().itemID = item.Value["id"].ToString();
            }

            StoreAssetmanager.Instance.itemsAvailable = itemsListData["data"];
        }
    }
}

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

    private bool dataLoaded = false;

    [SerializeField]
    private TMP_Text loadingText;
    [SerializeField]
    private GameObject background;

    void Start()
    {
        StartCoroutine(getData());
    }

    private void Update()
    {
        if (dataLoaded)
        {
            loadingText.gameObject.SetActive(false);
            background.SetActive(false);
        }
        else
        {
            StartCoroutine(loadingAnim());
        }
    }

    IEnumerator loadingAnim()
    {
        loadingText.text = "Loading";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading.";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading..";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading...";
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator getData()
    {
        Debug.Log("yo");
        UnityWebRequest itemsListRequest = UnityWebRequest.Get(AvailableRoutes.availableItems);

        dataLoaded = false;

        yield return itemsListRequest.SendWebRequest();

        dataLoaded = true;

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

    }
}

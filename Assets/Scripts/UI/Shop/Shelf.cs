using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shelf : MonoBehaviour
{
    public GameObject ItemTemplate;
    public TextAsset csvFile;

    void Start()
    {
        string[] items = csvFile.text.Split('\n');
        GameObject g;
        for (int i = 1; i < items.Length-1; i++)
        {
            string[] data = items[i].Split(',');
            g = Instantiate(ItemTemplate, transform);
            g.transform.position = new Vector3(g.transform.position[0]+150*(i-1), g.transform.position[1], g.transform.position[2]);
            g.gameObject.SetActive(true);
            g.transform.GetChild(0).GetComponent<Text>().text = data[1];
            g.GetComponent<TooltipTrigger>().itemID = data[0];
        }
    }
}

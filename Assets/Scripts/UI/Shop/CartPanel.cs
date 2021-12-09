using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartPanel : MonoBehaviour
{
    public RectTransform PanelRect;
    public GameObject ItemTemplate;

    public static bool remove = false;

    void Start()
    {
        ItemTemplate = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        int n = transform.childCount-1, m = Store.Items.Count;
        if (remove)
        {
            bool bb = true;
            foreach (Transform child in transform)
            {
                if (bb)
                {
                    bb = false;
                    continue;
                }
                Destroy(child.gameObject);
            }
            GameObject g;
            for (int i = 0; i < m; i++)
            {
                g = Instantiate(ItemTemplate, transform);
                g.transform.GetChild(0).GetComponent<Text>().text = Store.Items[i];
                g.transform.GetChild(1).gameObject.GetComponent<RemoveItem>().index = i;
            }
            remove = false;
        }
        if (n<m)
        {
            GameObject g;
            for (int i=n;i<m;i++)
            {
                g = Instantiate(ItemTemplate, transform);
                Debug.Log(Store.Items[i]);
                g.transform.GetChild(0).GetComponent<TMP_Text>().text = Store.Items[i];
                g.transform.GetChild(1).gameObject.GetComponent<RemoveItem>().index = i;
            }
        }

        bool b = true;
        foreach (Transform child in transform)
        {
            if (b)
            {
                b = false;
                continue;
            }
            child.gameObject.SetActive(PanelRect.rect.height > 450);
        }
    }

}

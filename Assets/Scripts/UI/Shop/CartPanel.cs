using TMPro;
using UnityEngine;

public class CartPanel : MonoBehaviour
{
    public RectTransform PanelRect;
    public GameObject ItemTemplate;
    public GameObject items;

    public static bool remove = false;

    void Update()
    {
        if (remove)
        {
            itemRemoved();
        }
        else
        {
            showItems();
        }
    }


    void showItems()
    {
        int n = items.transform.childCount - 1, m = Store.Items.Count;
        if (n < m)
        {
            GameObject g;
            for (int i = n; i < m; i++)
            {
                g = Instantiate(ItemTemplate, items.transform);
                g.transform.GetChild(0).GetComponent<TMP_Text>().text = Store.Items[i];
                g.transform.GetChild(1).gameObject.GetComponent<RemoveItem>().index = i;
            }
        }

        bool firstElement = true;
        foreach (Transform child in items.transform)
        {
            if (firstElement)
            {
                firstElement = false;
                continue;
            }
            child.gameObject.SetActive(!OpenCart.isPanelOpen);
        }
    }
    void itemRemoved()
    {
        bool firstElement = true;
        foreach (Transform child in items.transform)
        {
            if (firstElement)
            {
                firstElement = false;
                continue;
            }
            //print("Child name");
            Destroy(child.gameObject);
        }
        remove = false;
    }
}

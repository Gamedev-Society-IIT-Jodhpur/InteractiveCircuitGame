using System;
using TMPro;
using UnityEngine;

public class ShopItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    Transform quantity;
    Transform addToCart;
    Transform price;

    string quantityInt;
    void Start()
    {
        quantity = transform.Find("Name").transform.Find("Quantity");
        addToCart = transform.Find("ComponentName");
        price = transform.Find("BottomBar").transform.Find("Price");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increaseItemCount()
    {
        quantityInt = quantity.GetComponent<TextMeshProUGUI>().text;
        quantity.GetComponent<TextMeshProUGUI>().text = "" + Convert.ToInt16(quantityInt) + 1;
        //item.transform.SetParent(this.transform);
    }
    public void decreaseItemCount()
    {
        quantityInt = quantity.GetComponent<TextMeshProUGUI>().text;
        quantity.GetComponent<TextMeshProUGUI>().text = "" + Convert.ToInt16(quantityInt) + (-1);
    }
}

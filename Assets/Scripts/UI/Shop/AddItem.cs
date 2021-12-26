using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddItem : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI header;
    //public TMP_Dropdown valuesDropDown;
    //public TMP_InputField quantity;
    [SerializeField] TMP_Text quantityText;
    int quantity=1;
    string unit;
    string value;
    string price;
    string componentName;
    public static List<StaticData.ComponentData> tempInventory;

    private void Start()
    {

        tempInventory = new List<StaticData.ComponentData>();
        quantityText.text = quantity.ToString();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        foreach (var item in StoreAssetmanager.Instance.itemsAvailable)
        {
            if (item.Value["id"].ToString() == TooltipSystem.getItemID())
            {
                componentName = item.Value["name"];
                unit = item.Value["unit"];
                value = item.Value["value"];
                price = item.Value["price"];
                break;
            }
        }

        string s = "{0} - {1} - {2} ({3})";
        //string itemDesc = string.Format(s, header.text, value, unit, quantity.text);
        string itemDesc = string.Format(s, header.text, value, unit, quantity);

        Store.Items.Add(itemDesc);

        //int totalPrice = int.Parse(quantity.text) * int.Parse(price);
        int totalPrice = quantity * int.Parse(price);

        Checkout.totalAmount = (int.Parse(Checkout.totalAmount) + totalPrice).ToString();

        StaticData.ComponentData tempComponent = new StaticData.ComponentData();
        componentName =  StoreAssetmanager.Instance.itemsNameMaping[componentName];

        tempComponent.name = componentName;
        tempComponent.value = value;
        tempComponent.unit = unit;
        //tempComponent.quantity = int.Parse(quantity.text);
        tempComponent.quantity = quantity;
        tempComponent.price = price;

        tempInventory.Add(tempComponent);

    }

    public void IncreaseQuantity()
    {
        quantity += 1;
        quantityText.text = quantity.ToString();
    }

    public void DecreaseQuantity()
    {
        if (quantity > 0)
        {
            quantity -= 1;
            quantityText.text = quantity.ToString();
        }
    }
}

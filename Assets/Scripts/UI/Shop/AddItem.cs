using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddItem : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI header;
    //public TMP_Dropdown valuesDropDown;
    //public TMP_InputField quantity;
    public TMP_Text quantityText;
    public static int quantity = 1;
    string unit;
    string value;
    string price;
    public static string solderPrice;
    string componentName;
    public static List<StaticData.ComponentData> tempInventory;
    public static int breadboardCountInventroy = 0;
    public static int breadboardCountCart = 0;
    public static int solderingCountCart = 0;

    private void OnEnable()
    {
        quantityText.text = quantity.ToString();
    }

    private void Start()
    {
        tempInventory = new List<StaticData.ComponentData>();
        quantityText.text = quantity.ToString();

        for (int i = 0; i < StaticData.Inventory.Count; i++)
        {
            if (StaticData.Inventory[i].name == "breadboard")
            {
                breadboardCountInventroy = StaticData.Inventory[i].quantity;
                break;
            }
        }
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

        string s = "{0} - {1} - {2} ({3}) (Rs. {4})";
        //string itemDesc = string.Format(s, header.text, value, unit, quantity.text);
        string itemDesc = string.Format(s, header.text, value, unit, quantity, price);

        if (componentName == "Breadboard" && breadboardCountCart + breadboardCountInventroy < 1)
        {
            print("breadboard added");
            breadboardCountCart += 1;
            Store.Items.Add(itemDesc);
            int totalPrice = quantity * int.Parse(price);
            Checkout.totalAmount = (int.Parse(Checkout.totalAmount) + totalPrice).ToString();

            StaticData.ComponentData tempComponent = new StaticData.ComponentData();
            componentName = StoreAssetmanager.Instance.itemsNameMaping[componentName];

            tempComponent.name = componentName;
            tempComponent.value = value;
            tempComponent.unit = unit;
            //tempComponent.quantity = int.Parse(quantity.text);
            tempComponent.quantity = quantity;
            tempComponent.price = price;

            tempInventory.Add(tempComponent);
        }
        else if (componentName == "Breadboard" && breadboardCountCart > 0)
        {
            CustomNotificationManager.Instance.AddNotification(1, "Can't purchase more than 1 Breadboard. 1 already in cart.");
        }
        else if (componentName == "Breadboard" && breadboardCountInventroy > 0)
        {
            CustomNotificationManager.Instance.AddNotification(1, "Can't purchase more than 1 Breadboard. 1 already in Inventory");
        }
        else if (componentName == "Soldering Iron" && solderingCountCart==0 && !StaticData.isSolderingIron)
        {
            solderingCountCart += 1;
            Store.Items.Add(itemDesc);
            int totalPrice = quantity * int.Parse(price);
            solderPrice = price;
            Checkout.totalAmount = (int.Parse(Checkout.totalAmount) + totalPrice).ToString();
        }
        else if (componentName == "Soldering Iron" && solderingCountCart > 0)
        {
            CustomNotificationManager.Instance.AddNotification(1, "Can't purchase more than 1 Soldering Iron. 1 already in cart.");
        }
        else if (componentName == "Soldering Iron" && StaticData.isSolderingIron)
        {
            CustomNotificationManager.Instance.AddNotification(1, "Can't purchase more than 1 Soldering Iron. You have 1 Soldering Iron available in the Inventory");
        }
        else
        {
            print("some item added");
            Store.Items.Add(itemDesc);
            int totalPrice = quantity * int.Parse(price);
            Checkout.totalAmount = (int.Parse(Checkout.totalAmount) + totalPrice).ToString();

            StaticData.ComponentData tempComponent = new StaticData.ComponentData();
            componentName = StoreAssetmanager.Instance.itemsNameMaping[componentName];

            tempComponent.name = componentName;
            tempComponent.value = value;
            tempComponent.unit = unit;
            //tempComponent.quantity = int.Parse(quantity.text);
            tempComponent.quantity = quantity;
            tempComponent.price = price;

            tempInventory.Add(tempComponent);
        }

        //int totalPrice = int.Parse(quantity.text) * int.Parse(price);
        




    }

    public void IncreaseQuantity()
    {
        if (header.text == "Breadboard")
        {
            if (quantity >= 1)
            {
                quantity = 1;
                quantityText.text = quantity.ToString();
                //TODO add notification
                CustomNotificationManager.Instance.AddNotification(1, "Can't purchase more than 1 Breadboard");

            }

        }
        else if (header.text == "Soldering Iron")
        {
            if (quantity >= 1)
            {
                quantity = 1;
                quantityText.text = quantity.ToString();
                //TODO add notification
                CustomNotificationManager.Instance.AddNotification(1, "Can't purchase more than 1 Soldering Iron");

            }

        }
        else
        {
            quantity += 1;
            quantityText.text = quantity.ToString();
        }

    }

    public void DecreaseQuantity()
    {
        if (quantity > 1)
        {
            quantity -= 1;
            quantityText.text = quantity.ToString();
        }
    }
}

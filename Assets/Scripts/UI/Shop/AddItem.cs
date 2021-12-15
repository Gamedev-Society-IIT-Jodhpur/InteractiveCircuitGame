using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddItem : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI header;
    public TMP_Dropdown valuesDropDown;
    public TMP_InputField quantity;
    string unit;
    string value;
    string componentName;
    public static List<StaticData.ComponentData> tempInventory;

    private void Start()
    {
        tempInventory = new List<StaticData.ComponentData>();
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
                break;
            }
        }

        Debug.Log("Unit + Value" + unit + value);
        Debug.Log(componentName);

        string s = "{0} - {1} - {2} ({3})";
        string itemDesc = string.Format(s, header.text, value, unit, quantity.text);

        Store.Items.Add(itemDesc);

        StaticData.ComponentData tempComponent = new StaticData.ComponentData();
        componentName =  StoreAssetmanager.Instance.itemsNameMaping[componentName];

        tempComponent.name = componentName;
        tempComponent.value = value;
        tempComponent.unit = unit;
        tempComponent.quantity = int.Parse(quantity.text);

        tempInventory.Add(tempComponent);

    }
}

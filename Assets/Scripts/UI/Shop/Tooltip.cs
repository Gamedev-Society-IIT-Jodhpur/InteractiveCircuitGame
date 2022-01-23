using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public TMP_Text priceField;

    public LayoutElement layoutElement;

    public int characterLimit;

    public RectTransform rectTransform;

    public GameObject tooltip;

    public static string Price = "";
    public static string value = "";

    public TMP_Dropdown valueDropdown;
    List<string> m_DropOptions = new List<string> {};
    List<string> m_Prices = new List<string> {};

    private void Update()
    {
        Price = m_Prices[valueDropdown.value];
        value = valueDropdown.options[valueDropdown.value].text;
        priceField.text = "Rs. " + m_Prices[valueDropdown.value];
    }


    public void SetTooltip(string itemID)
    {
        foreach (var item in StoreAssetmanager.Instance.itemsAvailable)
        {
            if (item.Value["id"].ToString() == itemID)
            {
                headerField.text = item.Value["type"];
                contentField.text = item.Value["description"];
                string unit = item.Value["unit"];
                valueDropdown.ClearOptions();
                foreach (var val in item.Value["value"])
                {
                    //Debug.Log(val.Value[0]);
                    m_Prices.Add(val.Value[0]);
                    m_DropOptions.Add(val.Value[1] + " " + unit);
                }
                valueDropdown.AddOptions(m_DropOptions);
                //priceField.text = "Rs. " + item.Value["price"];
                
                break;
            }
        }


        Vector2 point = Input.mousePosition;

        float pivotX = point.x / Screen.width;
        float pivotY = point.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);


        transform.position = point;
    }

    void OnDisable()
    {
        m_DropOptions.Clear();
        m_Prices.Clear();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AddItem : MonoBehaviour, IPointerClickHandler
{

    public TextMeshProUGUI cartItems;
    public TextMeshProUGUI header;
    public TMP_Dropdown valuesDropDown;
    public TMP_InputField quantity;

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        int index = valuesDropDown.value;
        string value = valuesDropDown.options[index].text;
        if (cartItems.text.Equals("Empty"))
        {
            cartItems.text = header.text + " - " + value + " ( " + quantity.text + ")";
        }
        else
        {
            cartItems.text = cartItems.text + "\n" + header.text + " - " + value + " ( " + quantity.text + ")";
        }
        
    }
}

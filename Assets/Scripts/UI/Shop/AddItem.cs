using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AddItem : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI header;
    public TMP_Dropdown valuesDropDown;
    public TMP_InputField quantity;

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        int index = valuesDropDown.value;
        string value = valuesDropDown.options[index].text;

        Store.Items.Add(header.text + " - " + value + " ( " + quantity.text + ")");
    }
}

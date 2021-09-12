using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public TMP_Dropdown valuesDropDown;

    public LayoutElement layoutElement;

    public int characterLimit;

    public TextAsset csvFile;

    public RectTransform rectTransform;

    public GameObject tooltip;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Debug.Log("tooltip enter");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Debug.Log("tooltip exit");
        TooltipSystem.Hide();
    }

    public void SetTooltip(string itemID)
    {
        string[] data = csvFile.text.Split('\n');

        int k = -1;

        for (int  i=1; i < data.Length ; i++ )
        {
            if (data[i].Split(',')[0] == itemID)
            {
                k = i;
                break;
            }
        }

        if (k == -1)
        {
            headerField.text = "Invalid ID";
            valuesDropDown.options.RemoveAll(c => true);
            contentField.text = "No Content";
            return;
        }

        string[] item = data[k].Split(',');

        headerField.text = item[1];
        contentField.text = item[4];

        valuesDropDown.options.RemoveAll(c => true);
        string[] values = item[3].Trim('"').Split(' ');
        List<string> options = values.ToList();
        valuesDropDown.AddOptions(options);

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength>characterLimit || contentLength>characterLimit) ? true : false;

        Vector2 point = Input.mousePosition;

        float pivotX = point.x / Screen.width;
        float pivotY = point.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = point;
    }

}

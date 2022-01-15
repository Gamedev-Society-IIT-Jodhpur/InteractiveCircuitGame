using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public TMP_Text priceField;

    public LayoutElement layoutElement;

    public int characterLimit;

    public RectTransform rectTransform;

    public GameObject tooltip;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Debug.Log("tooltip enter");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TooltipSystem.Hide();
    }

    public void SetTooltip(string itemID)
    {
        foreach (var item in StoreAssetmanager.Instance.itemsAvailable)
        {
            if (item.Value["id"].ToString() == itemID)
            {
                string name = item.Value["name"];
                headerField.text = item.Value["name"];
                contentField.text = item.Value["description"];
                priceField.text = "Rs. " + item.Value["price"];
                break;
            }
        }


        Vector2 point = Input.mousePosition;

        float pivotX = point.x / Screen.width;
        float pivotY = point.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);


        transform.position = point;
    }

}

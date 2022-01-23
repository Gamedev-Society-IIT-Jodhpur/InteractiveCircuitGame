using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem current;

    public Tooltip tooltip;

    public static string item_ID;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string itemID)
    {

        AddItem.quantity = 1;
        current.tooltip.SetTooltip(itemID);
        current.tooltip.gameObject.SetActive(true);
        item_ID = itemID;
    }

    public static string getItemID()
    {
        return item_ID;
    }


    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
};
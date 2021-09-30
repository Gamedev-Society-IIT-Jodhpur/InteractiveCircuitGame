using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler, IPointerClickHandler
{

    public string itemID;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Debug.Log("item enter");
        TooltipSystem.Show(itemID);
    }

    /*public void OnPointerExit(PointerEventData pointerEventData)
    {
        //TooltipSystem.Hide();
        Debug.Log("item exit");
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //TooltipSystem.Show();
        Debug.Log("Click yo");
    }*/
}
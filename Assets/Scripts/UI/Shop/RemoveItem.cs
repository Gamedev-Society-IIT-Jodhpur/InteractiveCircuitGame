using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RemoveItem : MonoBehaviour, IPointerClickHandler
{
    public int index;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Store.Items.RemoveAt(index);
        CartPanel.remove = true;
    }
}

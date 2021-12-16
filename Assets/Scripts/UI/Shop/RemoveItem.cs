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
        int totalPrice = AddItem.tempInventory[index].quantity * int.Parse(AddItem.tempInventory[index].price);
        Checkout.totalAmount = (int.Parse(Checkout.totalAmount) - totalPrice).ToString();
        AddItem.tempInventory.RemoveAt(index);
        CartPanel.remove = true;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveItem : MonoBehaviour, IPointerClickHandler
{
    public int index;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Store.Items[index].Substring(0, 14) == "Soldering Iron")
        {
            AddItem.solderingCountCart = 0;
            Store.Items.RemoveAt(index);
            int totalPrice =int.Parse(AddItem.solderPrice);
            Checkout.totalAmount = (int.Parse(Checkout.totalAmount) - totalPrice).ToString();
            //AddItem.tempInventory.RemoveAt(index);
            CartPanel.remove = true;

        }
        else
        {
            if (Store.Items[index].Substring(0, 10) == "Breadboard")
            {
                AddItem.breadboardCountCart = 0;
            }

            Store.Items.RemoveAt(index);
            int totalPrice = AddItem.tempInventory[index].quantity * int.Parse(AddItem.tempInventory[index].price);
            Checkout.totalAmount = (int.Parse(Checkout.totalAmount) - totalPrice).ToString();
            AddItem.tempInventory.RemoveAt(index);
            CartPanel.remove = true;
        }
        
    }
}

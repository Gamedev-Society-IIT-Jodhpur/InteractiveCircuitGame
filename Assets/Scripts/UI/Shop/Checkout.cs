using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Checkout : MonoBehaviour
{

    public TMP_Text totalAmountText;
    public static string totalAmount = "0";


    private void Update()
    {
        totalAmountText.text = "Total : " + totalAmount;
    }

    public void CheckoutItems()
    {
        foreach(var items in AddItem.tempInventory)
        {
            StaticData.Inventory.Add(items);
        }

        Store.Items.Clear();
        AddItem.tempInventory.Clear();


        if (MoneyAndXPData.money <= int.Parse(totalAmount))
        {
            MoneyXPManager.DeductMoney(int.Parse(totalAmount));
            SceneManager.LoadScene("MAP");
        }
        else
        {
            CustomNotificationManager.Instance.AddNotification(2, "Not enough Money");
        }


    }
}

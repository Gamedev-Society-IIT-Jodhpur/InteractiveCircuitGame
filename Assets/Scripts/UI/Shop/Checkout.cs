using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkout : MonoBehaviour
{
    //TODO replace 100 with total amount 
    public void CheckoutItems()
    {
        foreach(var items in AddItem.tempInventory)
        {
            StaticData.Inventory.Add(items);
        }

        SceneManager.LoadScene("MAP");

        MoneyXPManager.DeductMoney(100);

    }
}

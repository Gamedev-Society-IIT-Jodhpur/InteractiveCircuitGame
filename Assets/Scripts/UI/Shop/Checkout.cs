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

        if (PrevCurrScene.curr <= 1)
        {
            PrevCurrScene.prev = PrevCurrScene.curr;
            PrevCurrScene.curr = 3;
        }
        //SceneManager.LoadScene("MAP");
        LoadingManager.instance.LoadGame(SceneIndexes.Shop, SceneIndexes.MAP);

        MoneyXPManager.DeductMoney(int.Parse(totalAmount));

    }
}

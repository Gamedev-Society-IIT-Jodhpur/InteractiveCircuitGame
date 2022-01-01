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
            bool added = false;
            for (int i = 0; i < StaticData.Inventory.Count; i++)
            {
                if(StaticData.Inventory[i].name== items.name && StaticData.Inventory[i].value == items.value)
                {
                    StaticData.ComponentData tempComponent = new StaticData.ComponentData();

                    tempComponent.name = StaticData.Inventory[i].name;
                    tempComponent.value = StaticData.Inventory[i].value;
                    tempComponent.unit = StaticData.Inventory[i].unit;
                    tempComponent.quantity = StaticData.Inventory[i].quantity+items.quantity;
                    tempComponent.price = StaticData.Inventory[i].price;
                    StaticData.Inventory.RemoveAt(i);
                    StaticData.Inventory.Add(tempComponent);
                    added = true;
                    break;
                }
                
            }
            if (!added)
            {
                StaticData.Inventory.Add(items);
            }


        }

        Store.Items.Clear();
        AddItem.tempInventory.Clear();

        if (PrevCurrScene.curr <= 1)
        {
            PrevCurrScene.prev = PrevCurrScene.curr;
            PrevCurrScene.curr = 3;
        }
        //SceneManager.LoadScene("Tinker");
        LoadingManager.instance.LoadGame(SceneIndexes.Shop, SceneIndexes.MAP);

        MoneyXPManager.DeductMoney(int.Parse(totalAmount));
        totalAmount = "0";

    }
}

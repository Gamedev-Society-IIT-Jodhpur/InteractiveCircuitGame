using TMPro;
using UnityEngine;
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
        foreach (var items in AddItem.tempInventory)
        {
            bool added = false;
            for (int i = 0; i < StaticData.Inventory.Count; i++)
            {
                if (StaticData.Inventory[i].name == items.name && StaticData.Inventory[i].value == items.value)
                {
                    StaticData.ComponentData tempComponent = new StaticData.ComponentData();

                    tempComponent.name = StaticData.Inventory[i].name;
                    tempComponent.value = StaticData.Inventory[i].value;
                    tempComponent.unit = StaticData.Inventory[i].unit;
                    tempComponent.quantity = StaticData.Inventory[i].quantity + items.quantity;
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

        if (AddItem.solderingCountCart > 0)
        {
            StaticData.isSolderingIron = true;
            
        }

        Store.Items.Clear();
        AddItem.tempInventory.Clear();

        if (PrevCurrScene.curr <= 1)
        {
            PrevCurrScene.prev = PrevCurrScene.curr;
            PrevCurrScene.curr = 3;
        }
        //SceneManager.LoadScene("Tinker");

        MoneyXPManager.DeductMoney(int.Parse(totalAmount));
        if (MoneyAndXPData.money - int.Parse(totalAmount) >= 0)
        {
            LoadingManager.instance.LoadGame(SceneIndexes.Shop, SceneIndexes.MAP);
            totalAmount = "0";
            AddItem.breadboardCountCart = 0;
        }

    }
}

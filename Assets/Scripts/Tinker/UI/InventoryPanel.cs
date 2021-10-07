using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public struct InventoryButtons
    {
        public string unit;
        public int quantity;
        public InventoryButton button;
    }

    List<StaticData.Component> Inventory;
    [SerializeField] GameObject button;
    public Dictionary<string, InventoryButtons> inventoryDict; //will be used to save current items in inventory....main purpose is for delete button

    // Start is called before the first frame update
    void Start()
    {
        inventoryDict = new Dictionary<string, InventoryButtons>() { };
        Inventory = StaticData.Inventory;
        for (int i = 0; i < Inventory.Count; i++)
        {
            GameObject newButton= Instantiate<GameObject>(button);
            newButton.transform.SetParent(gameObject.transform);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            
            newButton.GetComponent<InventoryButton>().component = Inventory[i].name;
            newButton.GetComponent<InventoryButton>().value = Inventory[i].value;
            newButton.GetComponent<InventoryButton>().quantity = Inventory[i].quantity;
            newButton.GetComponent<InventoryButton>().unit = Inventory[i].unit;

            InventoryButtons buttons;
            buttons.unit = Inventory[i].unit;
            buttons.quantity = Inventory[i].quantity;
            buttons.button = newButton.GetComponent<InventoryButton>();

            if (Inventory[i].name.Substring(0,7) != "voltage") //since icons of batteries has value already written in it.
            {
                inventoryDict[newButton.GetComponent<InventoryButton>().component+Inventory[i].value] = buttons;
            }
            else
            {
                inventoryDict[newButton.GetComponent<InventoryButton>().component] = buttons;
            }

        }
    }

}

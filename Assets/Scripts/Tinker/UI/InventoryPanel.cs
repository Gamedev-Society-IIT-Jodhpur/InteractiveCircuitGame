using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    List<StaticData.Component> Inventory;
    [SerializeField] GameObject button;
    Text[] childs;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = StaticData.Inventory;
        for (int i = 0; i < Inventory.Count; i++)
        {
            GameObject newButton= Instantiate<GameObject>(button);
            newButton.transform.SetParent(gameObject.transform);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.GetComponent<Button>().image.sprite = AssetManager.tinkerIconsDict[Inventory[i].name];
            childs = newButton.GetComponentsInChildren<Text>();
            childs[0].text = Inventory[i].quantity.ToString();
            childs[1].text = Inventory[i].value + Inventory[i].unit;

            newButton.GetComponent<InventoryButton>().component = Inventory[i].name;
            newButton.GetComponent<InventoryButton>().value = Inventory[i].value;
            newButton.GetComponent<InventoryButton>().quantity = Inventory[i].quantity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] List<GameObject> components;
    public Dictionary<string, GameObject> componentsDict;

    public string component;
    public string value;
    public string unit;
    public int quantity;
    Text[] childs;
    GameObject newComponent;

    private void Start()
    {
        componentsDict = new Dictionary<string, GameObject>(){
            { "voltage9",components[0]},
            { "breadboard",components[1]},
            { "led",components[2]},
            { "resistor",components[3]}/*,
            { "voltage1.5",components[4]}*/
        };
        childs = GetComponentsInChildren<Text>();
        childs[0].text = quantity.ToString();
        
        if (component.Substring(0, 7) != "voltage")
        {
            childs[1].text = value+unit;
        }
        GetComponent<Button>().image.sprite = AssetManager.tinkerIconsDict[component];

    }

    public void Instantiate()
    {
        newComponent = Instantiate(componentsDict[component]);

        if (newComponent.tag != "Breadboard")
        {
            newComponent.GetComponent<ComponentTinker>().value = value;
        }

        //update quatity in InventoryDict
        if (component.Substring(0, 7) == "voltage")
        {
            InventoryPanel.InventoryButtons buttons = GetComponentInParent<InventoryPanel>().inventoryDict[component];
            buttons.quantity -= 1;
            GetComponentInParent<InventoryPanel>().inventoryDict[component] = buttons;
        }
        else
        {
            InventoryPanel.InventoryButtons buttons = GetComponentInParent<InventoryPanel>().inventoryDict[component + value];
            buttons.quantity -= 1;
            GetComponentInParent<InventoryPanel>().inventoryDict[component + value] = buttons;
        }



        if (quantity == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateQuantity(quantity - 1);
            
        }
    }

    public void UpdateQuantity(int quantity)
    {
        this.quantity = quantity;
        childs[0].text = quantity.ToString();
    }




}

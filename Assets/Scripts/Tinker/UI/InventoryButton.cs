using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] List<GameObject> components;
    public Dictionary<string, GameObject> componentsDict;
    Dictionary<string, string> componentsNameDict;

    public string component;
    public string value;
    public string unit;
    public int quantity;
    TMP_Text[] childs;
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

        componentsNameDict = new Dictionary<string, string>(){
            { "voltage9","Battery"},
            { "breadboard","Breadboard"},
            { "led","LED"},
            { "resistor","Resistor"}/*,
            { "voltage1.5","Battery"}*/
        };
        childs = GetComponentsInChildren<TMP_Text>();
        childs[0].text = quantity.ToString();
        
        //if (component.Substring(0, 7) != "voltage")
        //{
            childs[1].text = value + " " + unit+" "+ componentsNameDict[component];
        //}
        GetComponentsInChildren<Image>()[1].sprite = AssetManager.tinkerComponentSpritesDict[component];

    }

    public void Instantiate()
    {
        if(!WireManager.isDrawingWire && !StaticData.isSoldering)
        {
            newComponent = Instantiate(componentsDict[component]);
            CircuitManagerTinker.ChangeSelected(newComponent);

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
        
    }

    public void UpdateQuantity(int quantity)
    {
        this.quantity = quantity;
        childs[0].text = quantity.ToString();
    }




}

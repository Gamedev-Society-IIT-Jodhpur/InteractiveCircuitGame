using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            { "resistor",components[3]},
            { "voltage1.5",components[4]},
            {"bjtnpn",components[5] },
            {"bjtpnp",components[6] },
            {"diode",components[7] },
            {"zenerDiode",components[8] },
            {"gizmo",components[9] },
        };

        componentsNameDict = new Dictionary<string, string>(){
            { "voltage9","Battery"},
            { "breadboard","Breadboard"},
            { "led","LED"},
            { "resistor","Resistor"},
            { "voltage1.5","Battery"},
            { "bjtnpn","NPN BJT"},
            { "bjtpnp","PNP BJT"},
            { "diode","Diode"},
            { "zenerDiode","Zener Diode"},
            { "gizmo","Gizmo"},
        };
        childs = GetComponentsInChildren<TMP_Text>();
        childs[0].text = quantity.ToString();

        /*if (component == "zenerDiode")
        {
            childs[1].text = "Breakdown Voltage = "+value + " " + unit + " " + componentsNameDict[component];
        }
        else
        {*/
            childs[1].text = value + " " + unit + " " + componentsNameDict[component];
        //}

        GetComponentsInChildren<Image>()[1].sprite = AssetManager.tinkerComponentSpritesDict[component];

    }

    public void Instantiate()
    {
        if (!WireManager.isDrawingWire && !StaticData.isSoldering)
        {
            newComponent = Instantiate(componentsDict[component]);


            if (newComponent.GetComponent<ComponentTinker>())
            {
                newComponent.GetComponent<ComponentTinker>().value = value;
                if (newComponent.tag == "BJT")
                {
                    newComponent.GetComponent<ComponentTinker>().beta = int.Parse(value);
                }
                //if (newComponent.GetComponent<ComponentTinker>().a == CircuitManagerTinker.component.zenerDiode)
                //{
                //    //newComponent.GetComponent<ComponentTinker>().beta = int.Parse(value.Substring(17));
                //    //print(value);
                //    //print(unit);
                //}
            }
            CircuitManagerTinker.ChangeSelected(newComponent);

            //update quatity in InventoryDict
            if (component.Length >= 7 && component.Substring(0, 7) == "voltage")
            {
                InventoryPanel.InventoryButtons buttons = GetComponentInParent<InventoryPanel>().inventoryDict[component];
                buttons.quantity -= 1;
                GetComponentInParent<InventoryPanel>().inventoryDict[component] = buttons;
            }
            else if (component.Length >= 10 && component.Substring(0, 10) == "breadboard")
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
        childs = GetComponentsInChildren<TMP_Text>();
        childs[0].text = quantity.ToString();
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] InventoryPanel inventoryPanel;
    [SerializeField] GameObject button;
    ComponentTinker[] breadboardComponents;
    NodeTinker[] nodes;

    public void Delete()
    {
        if (CircuitManagerTinker.selected && !StaticData.isSoldering)
        {
            GameObject selectedComponent = CircuitManagerTinker.selected;
            selectedComponent.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
            if (/*CircuitManagerTinker.selected.tag != "Breadboard" || */selectedComponent.GetComponent<ComponentTinker>())
            {
                if (CircuitManagerTinker.selected.transform.parent!=null && CircuitManagerTinker.selected.transform.parent.tag == "soldered")
                {
                    GameObject currentParent = CircuitManagerTinker.selected.transform.parent.gameObject;
                    //DeleteComponent(selectedComponent);
                    Drag[] connecteds = currentParent.GetComponentsInChildren<Drag>();
                    for (int i = 0; i < connecteds.Length; i++)
                    {
                        DeleteComponent(connecteds[i].gameObject);
                    }
                   // if (currentParent.GetComponentsInChildren<Drag>().Length == 0)
                    {
                        Destroy(currentParent);
                    }
                }
                else
                {
                    DeleteComponent(selectedComponent);
                }
            }
            else
            {
                breadboardComponents = selectedComponent.GetComponentsInChildren<ComponentTinker>();
                for (int i = 0; i < breadboardComponents.Length; i++) //delete each component connected to breadboard.
                {
                    DeleteComponent(breadboardComponents[i].gameObject);
                }

                string componentKey;
                if (selectedComponent.tag == "Breadboard")
                {
                    componentKey = "breadboard";
                }
                else
                {
                    componentKey = "gizmo";
                }

                //delete breadboard code...
                InventoryPanel.InventoryButtons buttons;
                if (inventoryPanel.inventoryDict[componentKey].quantity > 0)
                {
                    buttons = inventoryPanel.inventoryDict[componentKey];
                    buttons.quantity += 1;
                    buttons.button.UpdateQuantity(buttons.quantity);
                    inventoryPanel.inventoryDict[componentKey] = buttons;
                }
                else
                {
                    buttons = inventoryPanel.inventoryDict[componentKey];
                    buttons.quantity = 1;
                    GameObject newButton = Instantiate<GameObject>(button);
                    buttons.button = newButton.GetComponent<InventoryButton>();
                    buttons.button.transform.SetParent(inventoryPanel.transform);
                    buttons.button.transform.localScale = new Vector3(1, 1, 1);

                    buttons.button.value = "";
                    buttons.button.quantity = 1;
                    buttons.button.unit = "";
                    buttons.button.component = componentKey;
                    inventoryPanel.inventoryDict[componentKey] = buttons;
                }
                nodes = selectedComponent.GetComponentsInChildren<NodeTinker>();
                foreach (NodeTinker node in nodes)
                {
                    for (int i = node.wires.Count - 1; i >= 0; i--)
                    {
                        node.wires[i].GetComponentInParent<NewWireManager>().DestroyWire();
                    }

                }
                Destroy(selectedComponent);

            }
        }
        else if(!StaticData.isSoldering)
        {
            print("No component selected");
        }
    }



    public void DeleteComponent(GameObject component)
    {
        ComponentTinker componentTinker = component.GetComponent<ComponentTinker>();
        CircuitManagerTinker.componentList.Remove(component);
        if ( componentTinker.isWorking)
        {
            string utilKey;
            if (component.tag != "BJT")
            {
                utilKey = componentTinker.a + componentTinker.value;
            }
            else if (component.GetComponent<ComponentTinker>().model==CircuitManagerTinker.model.BC547)
            {
                utilKey = "bjtnpn" + componentTinker.beta;
            }
            else
            {
                utilKey = "bjtpnp" + componentTinker.beta;
            }

            InventoryPanel.InventoryButtons buttons;
            if (inventoryPanel.inventoryDict[utilKey].quantity > 0)
            {
                buttons = inventoryPanel.inventoryDict[utilKey];
                buttons.quantity += 1;
                buttons.button.UpdateQuantity(buttons.quantity);
                inventoryPanel.inventoryDict[utilKey] = buttons;
            }
            else
            {
                buttons = inventoryPanel.inventoryDict[utilKey];
                buttons.quantity = 1;
                GameObject newButton = Instantiate<GameObject>(button);
                buttons.button = newButton.GetComponent<InventoryButton>();
                buttons.button.transform.SetParent(inventoryPanel.transform);
                buttons.button.transform.localScale = new Vector3(1, 1, 1);

                if (component.tag == "BJT") //bjt needs beta not value in place of button.value
                {
                    buttons.button.value = componentTinker.beta.ToString();
                }
                else
                {
                    buttons.button.value = componentTinker.value;
                }
                buttons.button.quantity = 1;
                buttons.button.unit = buttons.unit;

                if (componentTinker.a == CircuitManagerTinker.component.voltage)
                {
                    buttons.button.component = "voltage" + componentTinker.value;
                }
                else if (component.tag == "BJT")
                {
                    if (component.GetComponent<ComponentTinker>().model == CircuitManagerTinker.model.BC547)
                    {
                        buttons.button.component = "bjtnpn";
                    }
                    else
                    {
                        buttons.button.component = "bjtpnp";
                    }
                }
                else
                {
                    buttons.button.component = componentTinker.a.ToString();
                }
                inventoryPanel.inventoryDict[utilKey] = buttons;
            }
        }

        nodes = component.GetComponentsInChildren<NodeTinker>();
        foreach (NodeTinker node in nodes)
        {
            for (int i= node.wires.Count-1; i >= 0; i--)
            {
                node.wires[i].GetComponentInParent<NewWireManager>().DestroyWire();
            }
            
        }
        Destroy(component);
    }
}

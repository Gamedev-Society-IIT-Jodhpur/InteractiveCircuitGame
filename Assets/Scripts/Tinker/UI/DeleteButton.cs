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
        if (CircuitManagerTinker.selected)
        {
            GameObject selectedComponent = CircuitManagerTinker.selected;
            selectedComponent.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
            if (CircuitManagerTinker.selected.tag != "Breadboard")
            {
                DeleteComponent(selectedComponent);
            }
            else
            {
                breadboardComponents = selectedComponent.GetComponentsInChildren<ComponentTinker>();
                for (int i = 0; i < breadboardComponents.Length; i++) //delete each component connected to breadboard.
                {
                    DeleteComponent(breadboardComponents[i].gameObject);
                }

                //delete breadboard code...
                InventoryPanel.InventoryButtons buttons;
                if (inventoryPanel.inventoryDict["breadboard"].quantity > 0)
                {
                    buttons = inventoryPanel.inventoryDict["breadboard"];
                    buttons.quantity += 1;
                    buttons.button.UpdateQuantity(buttons.quantity);
                    inventoryPanel.inventoryDict["breadboard"] = buttons;
                }
                else
                {
                    buttons = inventoryPanel.inventoryDict["breadboard"];
                    buttons.quantity = 1;
                    GameObject newButton = Instantiate<GameObject>(button);
                    buttons.button = newButton.GetComponent<InventoryButton>();
                    buttons.button.transform.SetParent(inventoryPanel.transform);
                    buttons.button.transform.localScale = new Vector3(1, 1, 1);

                    buttons.button.value = "";
                    buttons.button.quantity = 1;
                    buttons.button.unit = "";
                    buttons.button.component = "breadboard";
                    inventoryPanel.inventoryDict["breadboard"] = buttons;
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
        else
        {
            print("No component selected");
        }
    }



    public void DeleteComponent(GameObject component)
    {
        ComponentTinker componentTinker = component.GetComponent<ComponentTinker>();
        CircuitManagerTinker.componentList.Remove(component);
        if (componentTinker.isWorking)
        {
            InventoryPanel.InventoryButtons buttons;
            if (inventoryPanel.inventoryDict[componentTinker.a + componentTinker.value].quantity > 0)
            {
                buttons = inventoryPanel.inventoryDict[componentTinker.a + componentTinker.value];
                buttons.quantity += 1;
                buttons.button.UpdateQuantity(buttons.quantity);
                inventoryPanel.inventoryDict[componentTinker.a + componentTinker.value] = buttons;
            }
            else
            {
                buttons = inventoryPanel.inventoryDict[componentTinker.a + componentTinker.value];
                buttons.quantity = 1;
                GameObject newButton = Instantiate<GameObject>(button);
                buttons.button = newButton.GetComponent<InventoryButton>();
                buttons.button.transform.SetParent(inventoryPanel.transform);
                buttons.button.transform.localScale = new Vector3(1, 1, 1);

                buttons.button.value = componentTinker.value;
                buttons.button.quantity = 1;
                buttons.button.unit = buttons.unit;

                if (componentTinker.a == CircuitManagerTinker.component.voltage)
                {
                    buttons.button.component = "voltage" + componentTinker.value;
                }
                else
                {
                    buttons.button.component = componentTinker.a.ToString();
                }
                inventoryPanel.inventoryDict[componentTinker.a + componentTinker.value] = buttons;
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

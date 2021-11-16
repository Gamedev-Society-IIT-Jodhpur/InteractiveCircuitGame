using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : MonoBehaviour
{
    NodeTinker[] nodes;

    public void Rotate()
    {
        if (CircuitManagerTinker.selected && !StaticData.isSoldering)
        {
            GameObject selectedComponent = CircuitManagerTinker.selected;
            
            if (selectedComponent.tag != "Breadboard")
            {
                if (CircuitManagerTinker.selected.transform.parent != null && CircuitManagerTinker.selected.transform.parent.tag == "soldered")
                {
                    selectedComponent = CircuitManagerTinker.selected.transform.parent.gameObject;
                    float z = selectedComponent.transform.eulerAngles.z;
                    z += 90;
                    selectedComponent.transform.rotation = Quaternion.Euler(0, 0, z);
                    selectedComponent.transform.parent = null;

                    nodes = selectedComponent.GetComponentsInChildren<NodeTinker>();
                    foreach (NodeTinker node in nodes)
                    {
                        foreach (GameObject wire in node.wires)
                        {
                            wire.GetComponent<Wire>().RotateWithWire();

                        }
                    }
                }
                else
                {
                    float z = selectedComponent.transform.eulerAngles.z;
                    z += 90;
                    selectedComponent.transform.rotation = Quaternion.Euler(0, 0, z);
                    selectedComponent.transform.parent = null;

                    nodes = selectedComponent.GetComponentsInChildren<NodeTinker>();
                    foreach (NodeTinker node in nodes)
                    {
                        foreach (GameObject wire in node.wires)
                        {
                            wire.GetComponent<Wire>().RotateWithWire();

                        }
                    }
                }


                
            }
            else
            {
                print("can't rotate breadboard");
            }

        }
        else
        {
            print("select a component to rotate");
        }
    }
}

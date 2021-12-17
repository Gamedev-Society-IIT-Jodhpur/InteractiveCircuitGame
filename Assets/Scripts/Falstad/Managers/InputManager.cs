using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public void ChangeValue(string value)
    {
        if (CircuitManager.selected.tag != "Gizmo")
        {
            ComponentInitialization component = CircuitManager.selected.GetComponent<ComponentInitialization>();
            if (component.tag != "Wire" && component.a != CircuitManager.component.bjt)
            {
                component.value = value;
                if (component.valueText)
                {
                    if (component.a == CircuitManager.component.resistor)
                    {

                        component.valueText.text = SIUnits.NormalizeRounded(Convert.ToDouble(value), 9, Char.ToString(((char)0x03A9)));
                    }
                    else if (component.a == CircuitManager.component.voltage)
                    {

                        component.valueText.text = SIUnits.NormalizeRounded(Convert.ToDouble(value), 9, "V");
                    }

                    else
                    {
                        component.valueText.text = "";
                    }
                }
            }
            else if (component.a == CircuitManager.component.bjt)
            {
                component.beta = int.Parse(value);
                component.valueText.text = "β=" + value;
                //print(value);

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField]TMP_InputField inputField;

    private void Start()
    {
        inputField.characterValidation = TMP_InputField.CharacterValidation.Decimal;
    }

    public void ChangeValue(string value)
    {
        if (CircuitManager.selected.tag != "Gizmo")
        {
            ComponentInitialization component = CircuitManager.selected.GetComponent<ComponentInitialization>();
            if (component.tag != "Wire" && component.a != CircuitManager.component.bjt && component.a!= CircuitManager.component.zenerDiode)
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
            else if (component.a == CircuitManager.component.bjt )
            {
                component.beta = int.Parse(value);
                component.valueText.text = "β=" + value;
                //print(value);

            }
            else if ( component.a == CircuitManager.component.zenerDiode)
            {
                component.beta = double.Parse(value);
                component.valueText.text = "BV=" + value;
                //print(value);

            }

        }
    }
}

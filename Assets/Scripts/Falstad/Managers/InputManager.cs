using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public void ChangeValue(string value)
    {
        ComponentInitialization component = CircuitManager.selected.GetComponent<ComponentInitialization>();
        if (component.tag != "Wire" && component.a !=CircuitManager.component.bjt)
        {
            component.value = value;
            component.valueText.text = value;
        }
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        //inputField.characterValidation = TMP_InputField.CharacterValidation.Decimal;
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        inputField.text = "";
        EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
        inputField.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OK();
        }
        if (inputField.gameObject.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }

    public void ChangeValue()
    {
        string value = inputField.text;
        if (inputField.text == "") return;
        if (CircuitManager.selected.tag != "Gizmo")
        {
            ComponentInitialization component = CircuitManager.selected.GetComponent<ComponentInitialization>();
            if (component.tag != "Wire" && component.a != CircuitManager.component.bjt && component.a != CircuitManager.component.zenerDiode)
            {
                component.value = value;
                if (component.valueText)
                {
                    if (component.a == CircuitManager.component.resistor)
                    {

                        component.valueText.text = SIUnits.NormalizeRounded(Convert.ToDouble(value), 2, Char.ToString(((char)0x03A9)));
                    }
                    else if (component.a == CircuitManager.component.voltage)
                    {

                        component.valueText.text = SIUnits.NormalizeRounded(Convert.ToDouble(value), 2, "V");
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
            else if (component.a == CircuitManager.component.zenerDiode)
            {
                component.beta = double.Parse(value);
                component.valueText.text = "V<sub>ZK</sub> = " + value+"V";
                //print(value);

            }

        }
    }


    public void OK()
    {
        if (inputField.text == "") return;
        else
        {
            ChangeValue();
            gameObject.SetActive(false);
        }
    }

    public void DisableOther()
    {
        GameObject[] editPanels = GameObject.FindGameObjectsWithTag(gameObject.tag);
        for (int i = 0; i < editPanels.Length; i++)
        {
            if (editPanels[i] != gameObject)
            {
                editPanels[i].SetActive(false);
            }
        }
        CircuitManager.ChangeSelected(GetComponentInParent<ComponentInitialization>().gameObject);
            

        
    }
}

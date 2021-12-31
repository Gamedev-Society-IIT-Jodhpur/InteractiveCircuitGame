using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using UnityEngine;
using TMPro;

public class ComponentInitialization : MonoBehaviour

{
    
    public CircuitManager.component a;
    public CircuitManager.model model;
    //public string a;
    public int no_nodes =2;
    public string nameInCircuit;
    public string value ;
    public double beta = 6;
    public List<string> nodes = new List<string>();
    Transform[] childs;
    public TMP_Text valueText;

    void Start()
    {
        CircuitManager.componentList.Add(gameObject);
        if (a!= CircuitManager.component.wire && a!= CircuitManager.component.bjt && a!=CircuitManager.component.diode && a!=CircuitManager.component.zenerDiode)
        {
            childs = GetComponentsInChildren<Transform>();
            valueText = GetComponentInChildren<TMP_Text>();
            if (valueText)
            {
                
                if (a == CircuitManager.component.resistor)
                {

                    
                    valueText.text = SIUnits.NormalizeRounded(Convert.ToDouble(value), 9, Char.ToString(((char)0x03A9) ));
                }
                else if (a == CircuitManager.component.voltage)
                {

                    valueText.text = SIUnits.NormalizeRounded(Convert.ToDouble(value) , 9,"V");
                }
                
                else
                {
                    valueText.text = "";
                }
            }

        }
        else if (a == CircuitManager.component.bjt)
        {
            value = model.ToString();
            valueText = GetComponentInChildren<TMP_Text>();
            valueText.text = "β=" + beta.ToString();
        }
        else if (a == CircuitManager.component.diode)
        {
            value = "Default";
        }
        else if (a == CircuitManager.component.zenerDiode)
        {
            value = "ZenerDiode";
            valueText = GetComponentInChildren<TMP_Text>();
            valueText.text = "BV=" + beta.ToString();
        }
       // print(a.ToString());


    }
    
    public void Initialize(int i, List<string> nodes)
    {

        
        UnifiedScript.dict1[a.ToString()].DynamicInvoke(a.ToString() + i,  nodes, value , beta.ToString());
        nameInCircuit = a.ToString() + i;

    }

     
    
}

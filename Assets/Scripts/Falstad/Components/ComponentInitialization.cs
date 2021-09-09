using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInitialization : MonoBehaviour

{
    
    public CircuitManager.component a;
    //public string a;
    public int no_nodes =2;
    public string nameInCircuit;
    public string value ;
    public List<string> nodes = new List<string>();
    Transform[] childs;
    public Text valueText;

    void Start()
    {
        CircuitManager.componentList.Add(gameObject);
        if (a!= CircuitManager.component.wire && a!= CircuitManager.component.bjt && a!=CircuitManager.component.diode)
        {
            childs = GetComponentsInChildren<Transform>();
            valueText = childs[childs.Length - 1].GetComponent<Text>();
            valueText.text = value;

        }
        else if (a == CircuitManager.component.bjt)
        {
            value = "mjd44h11";
        }
        else if (a == CircuitManager.component.diode)
        {
            value = "1N914";
        }
        print(a.ToString());


    }
    
    public void Initialize(int i, List<string> nodes)
    {

        
        UnifiedScript.dict1[a.ToString()].DynamicInvoke(a.ToString() + i,  nodes, value);
        nameInCircuit = a.ToString() + i;

    }

     
    
}

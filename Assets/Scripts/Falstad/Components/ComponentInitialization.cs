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
        if (a!= CircuitManager.component.wire && a!= CircuitManager.component.bjt)
        {
            childs = GetComponentsInChildren<Transform>();
            valueText = childs[childs.Length - 1].GetComponent<Text>();
            valueText.text = value;

        }
        else if (a == CircuitManager.component.bjt)
        {
            value = "mjd44h11";
        }
        print(a.ToString());


    }
    
    public void Initialize(int i, List<string> nodes)
    {

        //print("value line 22 of ComponentInitialization " + value);
        //if (a != "bjt")
        //{
        UnifiedScript.dict1[a.ToString()].DynamicInvoke(a.ToString() + i,  nodes, value);
        nameInCircuit = a.ToString() + i;
       // }
       /* else
        {
            childs = gameObject.GetComponentsInChildren<Transform>();
            //value = (Mathf.RoundToInt(childs[3].position.x)).ToString() + " " + (Mathf.RoundToInt(childs[3].position.y)).ToString();
            UnifiedScript.dict1[a].DynamicInvoke(a + i, nodes ,value);
            nameInCircuit = a + i;

        }*/
    }

     
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInitialization : MonoBehaviour

{
    public string a;
    public int no_nodes =2;
    public string nameInCircuit;
    public string value ;
    public List<string> nodes = new List<string>();
    Transform[] childs;
    public Text valueText;

    
    void Start()
    {
        CircuitManager.componentList.Add(gameObject);
        if (gameObject.tag != "Wire" && a!="bjt")
        {
            childs = GetComponentsInChildren<Transform>();
            valueText = childs[childs.Length - 1].GetComponent<Text>();
            valueText.text = value;
        }
        else if (a == "bjt")
        {
            value = "mjd44h11";
        }

    }

    public void Initialize(int i, List<string> nodes)
    {

        //print("value line 22 of ComponentInitialization " + value);
        //if (a != "bjt")
        //{
            UnifiedScript.dict1[a].DynamicInvoke(a + i,  nodes, value);
            nameInCircuit = a + i;
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

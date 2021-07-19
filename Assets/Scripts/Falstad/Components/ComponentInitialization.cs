using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInitialization : MonoBehaviour

{
    public string a;
    public string nameInCircuit;
    public string value = "100";
    public string pos = "0";
    public string neg = "0";
    Transform[] childs;
    public Text valueText;
    
    // Start is called before the first frame update
    void Start()
    {
        CircuitManager.componentList.Add(gameObject);
        if (gameObject.tag != "Wire" && a!="bjt")
        {
            childs = GetComponentsInChildren<Transform>();
            valueText = childs[childs.Length - 1].GetComponent<Text>();
            valueText.text = value;
        }

    }

    public void Initialize(int i,string pos,string neg)
    {

        //print("value line 22 of ComponentInitialization " + value);
        if (a != "bjt")
        {
            UnifiedScript.dict1[a].DynamicInvoke(a + i, pos, neg, value);
            nameInCircuit = a + i;
        }
        else
        {
            childs = gameObject.GetComponentsInChildren<Transform>();
            value = (Mathf.RoundToInt(childs[3].position.x)).ToString() + " " + (Mathf.RoundToInt(childs[3].position.y)).ToString();
            UnifiedScript.dict1[a].DynamicInvoke(a + i, value, pos, neg);
            nameInCircuit = a + i;

        }
    }

     
    
}

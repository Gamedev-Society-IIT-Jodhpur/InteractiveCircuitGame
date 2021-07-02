using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentInitialization : MonoBehaviour

{
    public string a;
    public string nameInCircuit;
    [SerializeField]string value = "100";
    public string pos = "0";
    public string neg = "0";
    
    // Start is called before the first frame update
    void Start()
    {
        CircuitManager.componentList.Add(gameObject);
        
    }

    public void Initialize(int i,string pos,string neg)
    {
        //print("value line 22 of ComponentInitialization " + value);
        UnifiedScript.dict1[a].DynamicInvoke(a+i,pos,neg,value);
        nameInCircuit = a+i;
    }

     
    
}

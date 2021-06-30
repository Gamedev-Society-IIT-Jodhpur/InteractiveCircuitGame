using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentInitialization : MonoBehaviour

{
    public string a;
    public string value = "1";
    private string pos = "0";
    private string neg = "0";
    // Start is called before the first frame update
    void Start()
    {
        CircuitManager.componentList.Add(gameObject);
    }
    public void Initialize(int i)
    {
       
        UnifiedScript.dict1[a].DynamicInvoke(a+i ,value,pos,neg);

    }
     
    public void DeleteComponent()
    {
        CircuitManager.componentList.Remove(gameObject);
    }
}

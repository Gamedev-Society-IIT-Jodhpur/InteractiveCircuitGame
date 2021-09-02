using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentTinker : MonoBehaviour

{
    public CircuitManagerTinker.component a;
    public int no_nodes = 2;
    public string nameInCircuit;
    public string value;
    public List<string> nodes = new List<string>();
    public Transform[] childs;

    void Start()
    {
        CircuitManagerTinker.componentList.Add(gameObject);
        if (a == CircuitManagerTinker.component.bjt)
        {
            value = "mjd44h11";
        }
        if (a != CircuitManagerTinker.component.wire)
        {
            childs = GetComponentsInChildren<Transform>();
        }

    }

    public void Initialize(int i, List<string> nodes)
    {
        UnifiedScript.dict1[a.ToString()].DynamicInvoke(a.ToString() + i, nodes, value);
        nameInCircuit = a.ToString() + i;
    }
}

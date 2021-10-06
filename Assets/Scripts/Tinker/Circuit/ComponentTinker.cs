using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentTinker : MonoBehaviour

{
    public CircuitManagerTinker.component a;
    public CircuitManagerTinker.model model;
    public int no_nodes = 2;
    public string nameInCircuit;
    public string value;
    public int beta = 100;
    public List<string> nodes = new List<string>();
    public Transform[] childs;

    void Start()
    {
        CircuitManagerTinker.componentList.Add(gameObject);
        if (a == CircuitManagerTinker.component.bjt)
        {
            value = model.ToString();
        }
        if (a != CircuitManagerTinker.component.wire)
        {
            childs = GetComponentsInChildren<Transform>();
        }
        else if (a == CircuitManagerTinker.component.diode)
        {
            value = "Default";
        }

    }

    public void Initialize(int i, List<string> nodes)
    {
        UnifiedScript.dict1[a.ToString()].DynamicInvoke(a.ToString() + i, nodes, value , beta.ToString());
        nameInCircuit = a.ToString() + i;
    }
}

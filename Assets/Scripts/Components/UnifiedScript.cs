using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;

public class UnifiedScript: MonoBehaviour
{
    public string a;
    public  delegate void  Del();
    public static void  ResistorInitialize()
    {
        
        Debug.Log("yay working ");
    }

    

    
    System.Collections.Generic.Dictionary<string, System.Delegate> dict1 = new Dictionary<string, System.Delegate>();
    
    // Start is called before the first frame update
    void Start()
    {
        Del Resistordel = ResistorInitialize;
        dict1.Add("res", Resistordel);
        CircuitManager.componentList.Add(gameObject);

    }
    public void Initialize()
    {
        dict1[a].DynamicInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

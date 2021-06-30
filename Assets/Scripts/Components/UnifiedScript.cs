using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;

public class UnifiedScript: MonoBehaviour
{
    
    public  delegate void  Del();
    public static void  ResistorInitialize()
    {
        
        Debug.Log("yay working ");
    }




    public static System.Collections.Generic.Dictionary<string, System.Delegate> dict1 = new Dictionary<string, System.Delegate>();

    // Start is called before the first frame update
    void Awake()
    {
        Del Resistordel = ResistorInitialize;
       dict1.Add("res", Resistordel);
        CircuitManager.componentList.Add(gameObject);

    }
    

    // Update is called once per frame
    
}

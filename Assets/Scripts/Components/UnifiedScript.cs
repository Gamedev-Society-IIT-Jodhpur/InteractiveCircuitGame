using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;

public class UnifiedScript: MonoBehaviour
{
    
    public  delegate void  Del(string name ,string value , string pos , string neg);
    public static void  ResistorInitialize(string name ,string value , string pos , string neg )
    {
        
        
        Debug.Log("yay working ");
        CircuitManager.ckt.Add(new Resistor(name, pos, neg, double.Parse(value)));
    }




    public static System.Collections.Generic.Dictionary<string, System.Delegate> dict1 = new Dictionary<string, System.Delegate>();

    // Start is called before the first frame update
    void Awake()
    {
        Del Resistordel = ResistorInitialize;
       dict1.Add("res", Resistordel);
       

    }
    

    // Update is called once per frame
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;

public class UnifiedScript: MonoBehaviour
{
    
    public  delegate void  Del(string name, string pos , string neg, string value);
    public static Dictionary<string, System.Delegate> dict1 = new Dictionary<string, System.Delegate>();

    public static void  ResistorInitialize(string name , string pos , string neg , string value )
    {
        //Debug.Log("resistor value line 15 of UnifiedScript : "+value);
        CircuitManager.ckt.Add(new Resistor(name, pos, neg, double.Parse(value)));
    }
    public static void  VoltageInitialize(string name, string pos, string neg, string value )
    {
        //Debug.Log("yay working ");
        CircuitManager.ckt.Add(new VoltageSource(name, pos, neg, double.Parse(value)));
    }

    void Awake()
    {
        Del Resistordel = ResistorInitialize;
       dict1.Add("res", Resistordel);
        Del Voltagedel = VoltageInitialize;
        dict1.Add("Volt", Voltagedel);
    }
    
}

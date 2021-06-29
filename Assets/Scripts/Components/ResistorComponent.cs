using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;

public class ResistorComponent : MonoBehaviour
{

    private string pos = "0";
    private string neg = "0";
    //String x = "sssss";
    

    private void Awake(){
        
        CircuitManager.ckt.Add(new Resistor("R"+CircuitManager.components["Resistor"], pos, neg, 2.0e3));

    }


}


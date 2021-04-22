using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ResistorComponent : MonoBehaviour
{
    
    public Resistor resistor;

    /// <summary>
    /// value of resistor 
    /// </summary>
    public float resistorValue = 1000;

    void Awake()
    {
        resistor = CIrcuitSim.sim.Create<Resistor>(resistorValue);
    }
}

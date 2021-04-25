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
    public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();

        resistor = Sim.sim.Create<Resistor>(resistorValue);
    }
}

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
    public void ConnectToWire(int leadNo, WireObject wire, int wireLeadNo)
    {
        if (leadNo == 0)
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(resistor.leadIn, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(resistor.leadIn, wire.wire.leadOut);
            }

        }
        else
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(resistor.leadOut, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(resistor.leadOut, wire.wire.leadOut);
            }
        }
    }

}

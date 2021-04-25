using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class VoltageEleme : MonoBehaviour
{
    // Start is called before the first frame update
    public VoltageInput voltage;
    
    public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();

        voltage = Sim.sim.Create<VoltageInput>(Voltage.WaveType.DC);
    }

}

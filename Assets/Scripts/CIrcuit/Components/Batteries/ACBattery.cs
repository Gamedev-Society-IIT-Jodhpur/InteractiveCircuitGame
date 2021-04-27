using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ACBattery : MonoBehaviour
{
    public ACVoltageSource ACVolt;
    public float value = 5.0f;
    
    GameObject  CircuitSim;
    CircuitSim Sim;

    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();
        // instantiating 2 lead AC voltage source
        ACVolt=Sim.sim.Create<ACVoltageSource>(value);
    }

}

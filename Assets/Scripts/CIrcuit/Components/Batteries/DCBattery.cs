using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DCBattery : MonoBehaviour
{
    public DCVoltageSource DCVolt;
    public float value = 10;
    GameObject  CircuitSim;
    CircuitSim Sim;

    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();
        DCVolt = Sim.sim.Create<DCVoltageSource>(value);
    }

}

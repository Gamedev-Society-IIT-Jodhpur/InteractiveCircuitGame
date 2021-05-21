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

    public void ConnectToWire(int leadNo, WireObject wire, int wireLeadNo)
    {
        if (leadNo == 0)
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(DCVolt.leadNeg, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(DCVolt.leadNeg, wire.wire.leadOut);
            }

        }
        else
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(DCVolt.leadPos, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(DCVolt.leadPos, wire.wire.leadOut);
            }

        }

    }

}

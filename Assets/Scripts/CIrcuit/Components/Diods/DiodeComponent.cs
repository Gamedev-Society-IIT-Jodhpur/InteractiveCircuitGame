using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DiodeComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public DiodeElm diode;
    public GameObject CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();

        diode = Sim.sim.Create<DiodeElm>();
    }

    public void ConnectToWire(int leadNo, WireObject wire, int wireLeadNo)
    {
        if (leadNo == 0)
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(diode.leadIn, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(diode.leadIn, wire.wire.leadOut);
            }

        }
        else
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(diode.leadOut, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(diode.leadOut, wire.wire.leadOut);
            }

        }

    }
}
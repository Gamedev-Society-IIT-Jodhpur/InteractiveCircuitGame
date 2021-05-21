using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ZenerDiode : MonoBehaviour
{
    // Start is called before the first frame update
    public ZenerElm zener;
    public GameObject CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();

        zener = Sim.sim.Create<ZenerElm>();
    }

    public void ConnectToWire(int leadNo, WireObject wire, int wireLeadNo)
    {
        if (leadNo == 0)
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(zener.leadIn, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(zener.leadIn, wire.wire.leadOut);
            }

        }
        else
        {
            if (wireLeadNo == 0)
            {
                Sim.sim.Connect(zener.leadOut, wire.wire.leadIn);
            }
            else
            {
                Sim.sim.Connect(zener.leadOut, wire.wire.leadOut);
            }

        }

    }
}
